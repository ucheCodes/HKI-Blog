using HKBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKBlog.Controllers
{
    public interface IUserController
    {
        Task<bool> AddUser(User user);
        Task<User> LoginUser(string email, string password);
        Task<List<User>> ReadAllUsers();
        Task<Boolean> IsEmailRegistered(string email);
        Task<bool> IsEmailOTP(int otp, string email);
        void SendUsersPassword(string email);
        void GetAndMailOTP(string email);
    }
}
