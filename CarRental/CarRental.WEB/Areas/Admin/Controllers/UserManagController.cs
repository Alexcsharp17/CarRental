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
        private IUserService DataAcessService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }
        // GET: Admin/UserManag
        public ActionResult GetUsers()
        {
            var users = DataAcessService.Users.Where(u => u.Role == "user");
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }
        public ActionResult GetManagers()
        {
            var users = DataAcessService.Users.Where(u => u.Role == "manager");
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }
        public ActionResult CreateManager()
        {
            return View();
        }
        //public ActionResult CreateManager(RegisterModel model)
        //{
        //    UserService.cre
        //    if (ModelState.IsValid)
        //    {
        //        UserDTO userDto = new UserDTO
        //        {
        //            Email = model.Email,
        //            Password = model.Password,
        //            Address = model.Address,
        //            Name = model.Name,
        //            Role = "manager"
        //        };
        //        OperationDetails operationDetails = await UserService.Create(userDto);
        //        if (operationDetails.Succedeed)
        //            return View("SuccessRegister");
        //        else
        //            ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
        //    }
        //    return View(model);
        //}
    }
}