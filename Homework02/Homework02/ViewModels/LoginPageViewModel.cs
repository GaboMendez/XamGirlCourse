using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace Homework02.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        // Properties
        public string User { get; set; }
        public string Password { get; set; }

        public bool BoolPassword { get; set; } = true;
        // Commands
        public DelegateCommand LoginCommand { get; set; }
        public DelegateCommand SignupCommand { get; set; }
        public DelegateCommand ShowPassword { get; set; }

        public LoginPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) 
            : base(navigationService, pageDialogService)
        {

            LoginCommand = new DelegateCommand(async () =>
            {
                if (String.IsNullOrEmpty(User) || String.IsNullOrEmpty(Password))
                {
                    await DialogService.DisplayAlertAsync("Fields can not be empty! \nTry again!", null, "Ok");
                }
                else
                {
                    await Task.Delay(1000);

                    // Navigate to Home
                    await NavigationService.NavigateAsync(new Uri($"/{Constants.Navigation}/{Constants.TabbedPage}?selectedTab={Constants.Discovery}", UriKind.Absolute));
                }
            });

            SignupCommand = new DelegateCommand(async () =>
            {
                await NavigationService.NavigateAsync(new Uri($"/{Constants.Signup}", UriKind.Relative));
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
