using HKBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKBlog.States
{
    public interface IStore
    {
        public State State();
        void LoginClick(bool clickVal);
        public void AddActiveUser(User user);
        void AddStateChangedListeners(Action? listeners);
        public void RemoveStateChangedListeners(Action? listeners);
    }
}
