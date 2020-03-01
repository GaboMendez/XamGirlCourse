using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Homework04.Models;
using MonkeyCache.FileStore;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Essentials;

namespace Homework04.ViewModels
{
    public class SignupPageViewModel : ViewModelBase
    {
        // Properties
        public User NewUser { get; set; }
        public bool CanExecute { get; set; } = true;
        public bool BoolPassword { get; set; } = true;
        public bool BoolConfirmPassword { get; set; } = true;

        // Commands
        public DelegateCommand SignupCommand { get; set; }
        public DelegateCommand LoginCommand { get; set; }
        public DelegateCommand ShowPasswordCommand { get; set; }
        public DelegateCommand ShowConfirmPasswordCommand { get; set; }

        public SignupPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) 
            : base(navigationService, pageDialogService)
        {
            NewUser = new User();

            SignupCommand = new DelegateCommand(async () => { await Signup(); });

            LoginCommand = new DelegateCommand(async () => { await Login(); });

            ShowPasswordCommand = new DelegateCommand(() => { BoolPassword = !BoolPassword; });

            ShowConfirmPasswordCommand = new DelegateCommand(() => { BoolConfirmPassword = !BoolConfirmPassword; });
        }

        private async Task Login()
        {
            if (CanExecute)
            {
                CanExecute = false;

                await NavigationService.NavigateAsync(new Uri($"/{Constants.Login}", UriKind.Relative));

                CanExecute = true;
            }
        }

        private async Task Signup()
        {
            if (CanExecute)
            {
                CanExecute = false;

                if (String.IsNullOrEmpty(NewUser.Email) || String.IsNullOrEmpty(NewUser.Username) || String.IsNullOrEmpty(NewUser.Password) || String.IsNullOrEmpty(NewUser.ConfirmPassword))
                {
                    await DialogService.DisplayAlertAsync("Fields can not be empty! \nTry again!", null, "Ok");
                }
                else
                {
                    if (NewUser.Password.Equals(NewUser.ConfirmPassword))
                    {
                        var value = await SecureStorage.GetAsync(NewUser.Username.ToLower());
                        if (value != null) 
                            await DialogService.DisplayAlertAsync("Sorry...", "This Username is Already Taken! \nTry Again!", "Ok");
                        else
                        {
                            // Navigate to Home
                            await SecureStorage.SetAsync(NewUser.Username.ToLower(), NewUser.Password);
                            NewUser = new User(NewUser.Email, NewUser.Username);
                            Barrel.Current.Add(key: "LastUserID", data: NewUser.ID, expireIn: TimeSpan.FromDays(7));
                            Barrel.Current.Add(key: NewUser.Username.ToLower(), data: NewUser, expireIn: TimeSpan.FromDays(7));

                            var userParameters = new NavigationParameters
                            {
                                { "User", NewUser }
                            };
                            await Task.Delay(200);
                            await NavigationService.NavigateAsync(new Uri($"/{Constants.Navigation}/{Constants.TabbedPage}?selectedTab={Constants.Contact}", UriKind.Absolute), userParameters);
                        }
                      
                    }
                    else
                        await DialogService.DisplayAlertAsync("Passwords do not match! \nTry again!", null, "Ok");
                }

                CanExecute = true;
            }
        }
    }
}
