using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Homework03.Models;
using MonkeyCache.FileStore;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Essentials;

namespace Homework03.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        // Properties
        public User ActualUser { get; set; }
        public bool BoolPassword { get; set; } = true;
        public bool CanExecute { get; set; } = true;

        // Commands
        public DelegateCommand LoginCommand { get; set; }
        public DelegateCommand SignupCommand { get; set; }
        public DelegateCommand ShowPasswordCommand { get; set; }

        public LoginPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
            : base(navigationService, pageDialogService)
        {
            ActualUser = new User();

            LoginCommand = new DelegateCommand(async () => { await Login(); });

            SignupCommand = new DelegateCommand(async () => { await Signup(); });

            ShowPasswordCommand = new DelegateCommand( () => { BoolPassword = !BoolPassword; });
        }

        private async Task Login()
        {
            if (CanExecute)
            {
                CanExecute = false;

                if (String.IsNullOrEmpty(ActualUser.Username) || String.IsNullOrEmpty(ActualUser.Password))
                {
                    await DialogService.DisplayAlertAsync("Fields can not be empty! \nTry again!", null, "Ok");
                }
                else
                {
                    // Navigate to Home
                    var password = await SecureStorage.GetAsync(ActualUser.Username.ToLower());
                    if (password != null)
                    {
                        if (password.Equals(ActualUser.Password))
                        {
                            var User = Barrel.Current.Get<User>(key: ActualUser.Username.ToLower());
                            if (User != null)
                            {

                                var userParameters = new NavigationParameters
                                {
                                    { "User", User }
                                };

                                await NavigationService.NavigateAsync(new Uri($"/{Constants.Navigation}/{Constants.TabbedPage}?selectedTab={Constants.Discovery}", UriKind.Absolute), userParameters);
                            }
                            else
                                await DialogService.DisplayAlertAsync("Invalid Login Credentials! \nTry again!", null, "Ok");
                        }
                        else
                            await DialogService.DisplayAlertAsync("Invalid Login Credentials! \nTry again!", null, "Ok");
                    }
                    else
                        await DialogService.DisplayAlertAsync("Invalid Login Credentials! \nTry again!", null, "Ok");
                }

                CanExecute = true;
            }
        }

        private async Task Signup()
        {
            if (CanExecute)
            {
                CanExecute = false;

                await NavigationService.NavigateAsync(new Uri($"/{Constants.Signup}", UriKind.Relative));

                CanExecute = true;
            }
        }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            if (Barrel.ApplicationId.Equals(""))
            {
                Barrel.ApplicationId = "AllUsers";
            }
            User = null;
            //Barrel.Current.EmptyAll();
            //SecureStorage.RemoveAll();
        }
    }
}
