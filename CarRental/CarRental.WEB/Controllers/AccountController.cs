﻿using CarRental.BLL.DTO;
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

namespace CarRental.WEB.Controllers
{

    // GET: Account
    public class AccountController : Controller
    {
        private IUserService UserService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }

        private IAuthenticationManager AuthenticationManager
            {
                get
                {
                    return HttpContext.GetOwinContext().Authentication;
                }
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
                await SetInitialDataAsync();
                if (ModelState.IsValid)
                {
                    UserDTO userDto = new UserDTO { Email = model.Email, Password = model.Password };
                    ClaimsIdentity claim = await UserService.Authenticate(userDto);
                
                    if (claim == null)
                    {
                        ModelState.AddModelError("", "Incorrect login/password or your account has been banned.");
                    }
                    
                    else
                    {
                        AuthenticationManager.SignOut();
                        AuthenticationManager.SignIn(new AuthenticationProperties
                        {
                            IsPersistent = true
                        }, claim);
                     
                  
                    
                        return RedirectToAction("Index", "Home");
                    
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
            [ValidateAntiForgeryToken]
            public async Task<ActionResult> Register(RegisterModel model)
            {
                await SetInitialDataAsync();
                if (ModelState.IsValid)
                {
                    UserDTO userDto = new UserDTO
                    {
                        Email = model.Email,
                        Password = model.Password,
                       
                        Name = model.Name,
                        Role = "user"
                    };
                    OperationDetails operationDetails = await UserService.Create(userDto);
                    if (operationDetails.Succedeed)
                        return View("SuccessRegister");
                    else
                        ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
                }
                return View(model);
            }
            private async Task SetInitialDataAsync()
            {
                await UserService.SetInitialData(new UserDTO
                {
                    Email = "somemail@mail.ru",
                    UserName = "somemail@mail.ru",
                    Password = "ad46D_ewr3",
                    Name = "Семен Семенович Горбунков",
                    
                    Role = "admin",
                }, new List<string> { "user", "admin","manager" });
            }
        }
    
}