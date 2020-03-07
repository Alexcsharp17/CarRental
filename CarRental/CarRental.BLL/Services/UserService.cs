using CarRental.BLL.DTO;
using CarRental.BLL.Infrastracture;
using CarRental.BLL.Interfaces;
using CarRental.BLL.Mappers;
using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using Microsoft.AspNet.Identity;
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


        public async Task<OperationDetails> Create(UserDTO userDto)
        {

            ApplicationUser user = await Database.UserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {

                user = new ApplicationUser { Email = userDto.Email, UserName = userDto.Email,
                    PassportNumb=userDto.PassportNumb,Banned=userDto.Banned, Name=userDto.Name
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

        // начальная инициализация бд
        public async Task SetInitialData(UserDTO adminDto, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await Database.RoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new ApplicationRole { Name = roleName };
                    await Database.RoleManager.CreateAsync(role);
                }
            }
            await Create(adminDto);
        }

        public void Dispose()
        {
            Database.Dispose();
        }

    

       

        
    }
}
