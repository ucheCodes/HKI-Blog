using HKBlog.Models;
using HKBlog.States.SubStates;

namespace HKBlog.States
{
    public class State
    {
        public LoginClicker LoginClicker { get; set; } = new LoginClicker(false);
        public ActiveUser ActiveUser { get; set; } = new ActiveUser(new User());
    }
    public class Store : IStore
    {
        private State state = new State();
        public State State() { return state; }
        #region Mutations
        public void LoginClick(bool clickVal)
        {
            state.LoginClicker = new LoginClicker(clickVal);
            BroadcastStateChanged();
        }
        public void AddActiveUser(User user)
        {
            state.ActiveUser = new ActiveUser(user);
            BroadcastStateChanged();
        }
        #endregion
        #region observer patterns
        //Define events that will listen for changes
        private Action? _listeners;
        public void AddStateChangedListeners(Action? listeners)
        {
            _listeners += listeners;
        }
        public void RemoveStateChangedListeners(Action? listeners)
        {
            _listeners -= listeners;
        }
        public void BroadcastStateChanged()
        {
            _listeners?.Invoke();
        }
        #endregion
    }
}