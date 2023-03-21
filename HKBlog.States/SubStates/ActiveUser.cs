using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKBlog.Models;

namespace HKBlog.States.SubStates
{
    public class ActiveUser
    {
        public User User { get; } = new User(); 
        public ActiveUser(User user)
        {
            User = user;
        }
    }
}
