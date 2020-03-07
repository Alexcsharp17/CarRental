using CarRental.BLL.DTO;
using CarRental.BLL.Infrastracture;
using CarRental.BLL.Interfaces;
using CarRental.WEB.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarRental.WEB.Areas.Admin.Controllers
{
    public class UserManagController : Controller
    {
        private IDatAcessService DatAcessService;
        public UserManagController(IDatAcessService serv)
        {
            DatAcessService = serv;
        }
        private IUserService UserService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }
        // GET: Admin/UserManag
        public ActionResult GetUsers()
        {
            var users = DatAcessService.Users;
            if (users.Count() == 0)
            {
                return HttpNotFound();
            }
            return View(users);
        }
        public ActionResult GetManagers()
        {
            var users = DatAcessService.Users.Where(u => u.Role == "manager");
            if (users.Count() == 0)
            {
                return HttpNotFound();
            }
            return View(users);
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

       
    }
}