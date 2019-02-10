using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PZ1.Commands;
using PZ1.EventArguments;
using PZ1.Xml;
using System.Configuration;
using PZ1.Models;
using System.IO;

namespace PZ1.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        private CustomXmlRW _customXmlRW;
        private string _usersFilename;

        private string _username;
        private string _password;

        public string Username
        {
            get { return _username; }
            set { SetField<string>(ref _username, value); }
        }

        public string Password
        {
            get { return _password; }
            set { SetField<string>(ref _password, value); }
        }

        public Dictionary<string, User> Users { get; set; }

        public MyICommand LoginCommand { get; private set; }        

        public delegate void LoginEventHandler(object source, LoginEventArgs args);

        public event LoginEventHandler LoggingIn;

        public MyICommand RegisterCommand { get; private set; }

        public delegate void RegisterEventHandler(object source, RegisterEventArgs args);

        public event RegisterEventHandler Registering;

        public LoginViewModel() { }

        public LoginViewModel(CustomXmlRW customXmlRW)
        {
            _customXmlRW = customXmlRW;
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;
            _usersFilename = ConfigurationManager.AppSettings["usersFile"];
            _usersFilename = _usersFilename.Replace("{AppDir}", projectDirectory);
            var users = _customXmlRW.DeSerializeObject<List<User>>(_usersFilename);
            if(users == null)
            {
                Users = new Dictionary<string, User>();
                _customXmlRW.SerializeObject<List<User>>(Users.Values.ToList(), _usersFilename);
            }
            else
            {
                Users = users.ToDictionary(u => u.Username, u => u);
            }

            LoginCommand = new MyICommand(OnLoggingIn);
            RegisterCommand = new MyICommand(OnRegistering);
        }

        protected virtual void OnLoggingIn()
        {
            User loginUser = new User(Username, Password);
            if (CheckIfExists(loginUser))
            {
                LoggingIn?.Invoke(this, new LoginEventArgs(Username, Password));
            }
        }

        protected virtual void OnRegistering()
        {
            User registeringUser = new User(Username, Password);
            if (!CheckIfExists(registeringUser))
            {
                Users.Add(registeringUser.Username, registeringUser);
                _customXmlRW.SerializeObject<List<User>>(Users.Values.ToList(), _usersFilename);
                Registering?.Invoke(this, new RegisterEventArgs(Username, Password));
            }
        }

        // Subscription to LoggedOut event from MainWindowViewModel
        // which in return subscribed to LoggedOut event from ContentViewModel
        public void OnLoggedOut(object source, EventArgs e)
        {
            Username = "";
            Password = "";
            Console.WriteLine("Logged out.");
        }

        private bool CheckIfExists(User user)
        {
            if(!string.IsNullOrWhiteSpace(user.Username))
            {
                return Users.Count > 0 ? Users.TryGetValue(user.Username, out User u) : false;
            }
            return false;
        }
    }
}
