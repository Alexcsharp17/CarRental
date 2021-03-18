using CarRental.BLL.DTO;
using CarRental.BLL.Infrastracture;
using CarRental.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.BLL.Interfaces
{
    /// <summary>
    /// Specify the methods which should be implemented in User Service
    /// </summary>
    public interface IUserService : IDisposable
    {
        Task<OperationDetails> Create(UserDTO userDto);
        Task<ClaimsIdentity> Authenticate(UserDTO userDto);
        bool IsInRole(string id, string name);
        Task<UserDTO> Find(string user, string pass);
        Task<UserDTO> FindByEmail(string email);
        Task<UserDTO> FindById(string id);
        Task<string> GenerateEmailConfirmationTokenAsync(string id);
        Task SendEmailAsync(string id, string subject, string body);
        Task UpdateAsync(UserDTO userdto);




    }
}
