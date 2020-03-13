using CarRental.BLL.Attributes;
using CarRental.BLL.DTO;
using CarRental.BLL.Infrastracture;
using CarRental.BLL.Interfaces;
using CarRental.WEB.Areas.Admin.Models;
using CarRental.WEB.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CarRental.WEB.Areas.Admin.Controllers
{
    [ExceptionLogger]
    [Authorize(Roles = "admin")]
    public class UserManagController : Controller
    {
        static IEnumerable<string> userss = new List<string>();
        private IDatAcessService DatAcessService;
        public UserManagController(IDatAcessService serv)
        {
            DatAcessService = serv;
            var us = DatAcessService.Users.Select(u => u.Name);
            userss = us;
        }
        private IUserService UserService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }
        public ActionResult UserIndex()
        {
            return View(DatAcessService.Users);
        }
        public PartialViewResult GetUsers(string id=null)
        {
            List<UserDTO> usrs = new List<UserDTO>();
            if (id == null)
            {
                return PartialView("~/Views/Home/EmptySear.cshtml");
            }
            else
            {
                id = id.Substring(0, id.Length - 1);
                string[] strid = id.Split(Convert.ToChar("|"));
                for (int i = 0; i < strid.Length; i++)
                {
                    usrs.Add(DatAcessService.Users.FirstOrDefault(u => u.Id == strid[i]));
                }
            }
            IEnumerable<UserDTO> users=usrs;
            //return PartialView("~/Views/Areas/Admin/UserManag/GetUsers",usrs);
            return PartialView("~/Views/Home/RendUsers.cshtml",users);
        }
       
      
        [HttpGet]
      public ActionResult EditUser(string id)
        {
        
            var user = DatAcessService.Users.FirstOrDefault(u => u.Id == id);
            
            return View(user);
        }
        [HttpPost]
        public ActionResult EditUser(UserDTO user)
        {
            if (ModelState.IsValid)
            {
                DatAcessService.CreateUser(user);
                return RedirectToAction("GetUsers", "UserManag", new { res = "changed" });
            }
            return View(user);
        }
      
        public ActionResult UserDetails(string id)
        {
            return View(DatAcessService.Users.FirstOrDefault(u => u.Id == id));
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {

            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO
                {
                    Email = model.Email,
                    Password = model.Password,

                    Name = model.Name,
                    Role = "manager"
                };
                OperationDetails operationDetails = await UserService.Create(userDto);
                if (operationDetails.Succedeed)
                    return View("SuccessRegister");
                else
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(model);
        }
        public ActionResult AutocompleteUsName(string term)
        {
            var models = userss.Where(a => a.ToLower().Contains(term.ToLower()))
                            .Select(a => new { value = a })
                            .Distinct();
            return Json(models, JsonRequestBehavior.AllowGet);
        }

    }
}