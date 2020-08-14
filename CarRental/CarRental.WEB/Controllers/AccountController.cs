using CarRental.BLL.DTO;
using CarRental.BLL.Infrastracture;
using CarRental.BLL.Interfaces;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;
using CarRental.WEB.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.DataProtection;

namespace CarRental.WEB.Controllers
{

    // GET: Account
    public class AccountController : Controller
    {
        private IUserService UserService
        {
            get { return HttpContext.GetOwinContext().GetUserManager<IUserService>(); }          
        }
       
        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }                                   
        }
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();       
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model, string returnUrl)
        {          
            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO { Email = model.Email, Password = model.Password,  };
                ClaimsIdentity claim = await UserService.Authenticate(userDto);
                UserDTO user = await UserService.FindByEmail(userDto.Email);
                if (user == null)
                {
                    ModelState.AddModelError("", "Account with this name doesn't exist");
                }
                else if (claim == null)
                {
                    if (user.Banned == true)
                    {
                        ModelState.AddModelError("", "Your account was banned!");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Incorrect login/password.");
                    }
                }                                                    
                else
                {                   
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = true }, claim);
                                                  
                    if (UserService.IsInRole(user.Id,"admin"))
                    {
                        return RedirectToAction("Index", "Dashboard", new { Area = "Admin" });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {                      
            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO
                {
                    Email = model.Email,
                    Password = model.Password,
                    Id = User.Identity.GetUserId(),
                    Name = model.Name,
                    Role = "user"
                };

                OperationDetails operationDetails = await UserService.Create(userDto);
               
                ClaimsIdentity claim = await UserService.Authenticate(userDto);
                if (operationDetails.Succedeed)
                {
                    UserDTO user = await UserService.FindByEmail(userDto.Email);
                    try
                    {
                        var code = await UserService.GenerateEmailConfirmationTokenAsync(user.Id);
                     


                        // создаем ссылку для подтверждения
                        var callbackUrl = Url.Action("ConfirmEm", "Account", new { userId = user.Id, code = code },
                                   protocol: Request.Url.Scheme);
                        // отправка письма
                        await UserService.SendEmailAsync(user.Id, "Confirmation of mail",
                                   "To finish your registration click the link:: <a href=\""
                                                                   + callbackUrl + "\">Finish registration</a>");
                    }
                    catch(Exception ex)
                    {

                        var mes = ex.Message;
                        var mess = ex.HelpLink;
                        var messs = ex.Source;

                       
                      
                    }
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    },claim);

                    return View("SuccessRegister");
                }
                else
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(model);
        }
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEm(string userId, string Email)
        {
            UserDTO user = await UserService.FindById(userId);
            
            //ClaimsIdentity claim = await UserService.Authenticate(user);
            if (user != null)
            {
                if (user.Id == userId)
                {
                    user.EmailConfirmed = true;
                        await UserService.UpdateAsync(user);
                        //AuthenticationManager.SignIn(new AuthenticationProperties
                        //{
                        //    IsPersistent = true
                        //}, claim);                   
                    return RedirectToAction("Index", "Home", new { ConfirmedEmail = user.Email });
                }
                else
                {
                    return RedirectToAction("Confirm", "Account", new { Email = user.Email });
                }
            }
            else
            {
                return RedirectToAction("Confirm", "Account", new { Email = "" });
            }
        }

    }
    
}