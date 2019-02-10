using PZ1.Commands;
using PZ1.EventArguments;
using PZ1.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace PZ1.ViewModels
{
    public class ContentViewModel : BindableBase
    {
        public string CurrentUsername { get; set; }
        public string CurrentPassword { get; set; }

        private IUnityContainer _container;
        private CustomXmlRW _customXmlRW;
        
        private ShowImagesViewModel _showImagesViewModel;
        private AddImageViewModel _addImageViewModel;
        private AccountDetailsViewModel _accountDetailsViewModel;
        private BindableBase _currentContentViewModel;

        public MyICommand<string> NavCommand { get; private set; }
        public MyICommand LogoutCommand { get; private set; }

        public delegate void LogoutEventHandler(object sender, EventArgs args);
        public event LogoutEventHandler LoggedOut;

        public ContentViewModel() { }

        public ContentViewModel(IUnityContainer container, CustomXmlRW customXmlRW)
        {
            _container = container;

            _customXmlRW = customXmlRW;

            _showImagesViewModel = _container.Resolve<ShowImagesViewModel>();
            _addImageViewModel = _container.Resolve<AddImageViewModel>();
            _accountDetailsViewModel = _container.Resolve<AccountDetailsViewModel>();

            CurrentContentViewModel = _showImagesViewModel;

            NavCommand = new MyICommand<string>(OnNav);
            LogoutCommand = new MyICommand(OnLoggingOut);
        }

        public BindableBase CurrentContentViewModel
        {
            get { return _currentContentViewModel; }
            set { SetField(ref _currentContentViewModel, value); }
        }

        private void OnNav(string destination)
        {
            switch (destination)
            {
                case "showImages":
                    CurrentContentViewModel = _showImagesViewModel;
                    break;
                case "addImage":
                    CurrentContentViewModel = _addImageViewModel;
                    break;
                case "accountDetails":
                    CurrentContentViewModel = _accountDetailsViewModel;
                    break;
            }
        }

        private void OnLoggingOut()
        {
            LoggedOut?.Invoke(this, EventArgs.Empty);
        }

        // Subscription to Registered event on MainWindowViewModel
        public void OnRegistered(object source, RegisterEventArgs e)
        {
            CurrentContentViewModel = _addImageViewModel;
            CurrentUsername = e.Username;
            CurrentPassword = e.Password;
        }
    }
}
