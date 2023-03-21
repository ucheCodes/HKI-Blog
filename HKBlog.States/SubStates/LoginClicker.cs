using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKBlog.States.SubStates
{
    public class LoginClicker
    {
        public bool IsClicked { get; } = false;
        public LoginClicker(bool isclicked)
        {
            IsClicked = isclicked;
        }
    }
}
