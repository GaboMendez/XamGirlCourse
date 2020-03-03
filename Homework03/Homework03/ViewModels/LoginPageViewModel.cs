using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace Homework03.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        // Properties
        public string User { get; set; }
        public string Password { get; set; }
        public bool BoolPassword { get; set; } = true;
        public bool CanExecute { get; set; } = true;

        // Commands
        public DelegateCommand LoginCommand { get; set; }
        public DelegateCommand SignupCommand { get; set; }
        public DelegateCommand ShowPassword { get; set; }

        public LoginPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) 
            : base(navigationService, pageDialogService)
        {

            LoginCommand = new DelegateCommand(async () =>
            {
                if (CanExecute)
                {
                    CanExecute = false;

                    if (String.IsNullOrEmpty(User) || String.IsNullOrEmpty(Password))
                    {
                        await DialogService.DisplayAlertAsync("Fields can not be empty! \nTry again!", null, "Ok");
                    }
                    else
                    {
                        await Task.Delay(400);

                        // Navigate to Home
                        await NavigationService.NavigateAsync(new Uri($"/{Constants.Navigation}/{Constants.TabbedPage}?selectedTab={Constants.Discovery}", UriKind.Absolute));
                    }

                    CanExecute = true;
                }
               
            });

            SignupCommand = new DelegateCommand(async () =>
            {
                if (CanExecute)
                {
                    CanExecute = false;

                    await NavigationService.NavigateAsync(new Uri($"/{Constants.Signup}", UriKind.Relative));

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
        }
    }
}
