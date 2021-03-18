using AutoMapper;
using CarRental.BLL.DTO;
using CarRental.BLL.IdentityLogic;
using CarRental.BLL.Infrastracture;
using CarRental.BLL.Interfaces;
using CarRental.DAL.EF;
using CarRental.DAL.Entities;
using CarRental.DAL.Identity;
using CarRental.DAL.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.BLL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork Database { get; set; }

        public UserService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public bool IsInRole(string id, string name)
        {
            return Database.UserManager.IsInRole(id, name);
        }
        public async Task<UserDTO> Find(string us, string pass)
        {

            ApplicationUser user = await Database.UserManager.FindAsync(us, pass);

            //ICollection<IdentityUserRole> rol = user.Roles;
            UserDTO userdto = null;
            if (user != null)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<ApplicationUser, UserDTO>());
                var mapper = new Mapper(config);
                userdto = mapper.Map<UserDTO>(user);

            }
            return userdto;
        }
        public async Task<UserDTO> FindByEmail(string email)
        {

            ApplicationUser user = await Database.UserManager.FindByEmailAsync(email);
            //ICollection<IdentityUserRole> rol = user.Roles;
            UserDTO userdto = null;
            if (user != null)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<ApplicationUser, UserDTO>());
                var mapper = new Mapper(config);
                userdto = mapper.Map<UserDTO>(user);

            }
            return userdto;
        }
        public async Task<UserDTO> FindById(string id)
        {

            ApplicationUser user = await Database.UserManager.FindByIdAsync(id);
            //ICollection<IdentityUserRole> rol = user.Roles;
            UserDTO userdto = null;
            if (user != null)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<ApplicationUser, UserDTO>());
                var mapper = new Mapper(config);
                userdto = mapper.Map<UserDTO>(user);

            }
            return userdto;
        }
        public async Task UpdateAsync(UserDTO userdto)
        {
            ApplicationUser userd = await Database.UserManager.FindByIdAsync(userdto.Id);

            userd.EmailConfirmed = userdto.EmailConfirmed;
            try
            {
                await Database.UserManager.UpdateAsync(userd);
            }
            catch (Exception ex)
            {

            }
        }
        public async Task<string> GenerateEmailConfirmationTokenAsync(string id)
        {


            var provider = new DpapiDataProtectionProvider("CarRental");

            var userMan = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationContext()));

            userMan.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(
                provider.Create("CarRental"));

            return await userMan.GenerateEmailConfirmationTokenAsync(id);
        }
        public async Task SendEmailAsync(string id, string subject, string body)
        {
            var userMan = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationContext()));
            userMan.EmailService = new EmailService();
            await userMan.SendEmailAsync(id, subject, body);
        }


        public async Task<OperationDetails> Create(UserDTO userDto)
        {

            ApplicationUser user = await Database.UserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {

                user = new ApplicationUser
                {
                    Email = userDto.Email,
                    UserName = userDto.Email,
                    PassportNumb = userDto.PassportNumb,
                    Banned = userDto.Banned,
                    Name = userDto.Name
                };
                var result = await Database.UserManager.CreateAsync(user, userDto.Password);

                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                // add role
                await Database.UserManager.AddToRoleAsync(user.Id, userDto.Role);

                await Database.SaveAsync();
                return new OperationDetails(true, "You have sucessfully completed registration", "");
            }
            else
            {
                return new OperationDetails(false, "User with this nick is have already created!", "Email");
            }
        }
        public static DAL.Identity.ApplicationUserManager Create(UserDTO userDto, IOwinContext context)
        {

            var manager = new DAL.Identity.ApplicationUserManager(
                       new UserStore<ApplicationUser>(context.Get<ApplicationContext>()));
            //..........................
            manager.EmailService = new EmailService();

            return manager;

        }

        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
        {
            ClaimsIdentity claim = null;
            // находим пользователя
            ApplicationUser user = await Database.UserManager.FindAsync(userDto.Email, userDto.Password);
            // авторизуем его и возвращаем объект ClaimsIdentity
            if (user != null && !user.Banned)
                claim = await Database.UserManager.CreateIdentityAsync(user,
                                   DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }

        public void Dispose()
        {
            Database.Dispose();
        }

    }
}
