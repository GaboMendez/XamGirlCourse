using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace Homework02.ViewModels
{
    public class SignupPageViewModel : ViewModelBase
    {
        // Properties
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public bool BoolPassword { get; set; } = true;
        public bool BoolConfirmPassword { get; set; } = true;
        public bool CanExecute { get; set; } = true;

        // Commands
        public DelegateCommand SignUpCommand { get; set; }
        public DelegateCommand LoginCommand { get; set; }
        public DelegateCommand ShowPassword { get; set; }
        public DelegateCommand ShowConfirmPassword { get; set; }

        public SignupPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) 
            : base(navigationService, pageDialogService)
        {

            SignUpCommand = new DelegateCommand(async () =>
            {
                if (CanExecute)
                {
                    CanExecute = false;

                    if (String.IsNullOrEmpty(Email) || String.IsNullOrEmpty(Username) || String.IsNullOrEmpty(Password) || String.IsNullOrEmpty(ConfirmPassword))
                    {
                        await DialogService.DisplayAlertAsync("Fields can not be empty! \nTry again!", null, "Ok");
                    }
                    else
                    {
                        if (Password.Equals(ConfirmPassword))
                        {
                            await Task.Delay(400);

                            // Navigate to Home
                            await NavigationService.NavigateAsync(new Uri($"/{Constants.Navigation}/{Constants.TabbedPage}?selectedTab={Constants.Discovery}", UriKind.Absolute));
                        }
                        else
                            await DialogService.DisplayAlertAsync("Passwords do not match! \nTry again!", null, "Ok");
                    }

                    CanExecute = true;
                }
           
            });

            LoginCommand = new DelegateCommand(async () =>
            {
                if (CanExecute)
                {
                    CanExecute = false;

                    await NavigationService.NavigateAsync(new Uri($"/{Constants.Login}", UriKind.Relative));

                    CanExecute = true;
                }
            });

            ShowPassword = new DelegateCommand(() =>
            {
                if (BoolPassword)
                {
                    BoolPassword = false;
                }
                else
                    BoolPassword = true;
            });

            ShowConfirmPassword = new DelegateCommand(() =>
            {
                if (BoolConfirmPassword)
                {
                    BoolConfirmPassword = false;
                }
                else
                    BoolConfirmPassword = true;
            });
        }
    }
}
