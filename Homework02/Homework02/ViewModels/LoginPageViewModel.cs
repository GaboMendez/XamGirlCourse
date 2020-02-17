using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Homework02.Models;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace Homework02.ViewModels
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

            ShowPasswordCommand = new DelegateCommand(() => { BoolPassword = !BoolPassword; });
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
                    await Task.Delay(400);
                    await NavigationService.NavigateAsync(new Uri($"/{Constants.Navigation}/{Constants.TabbedPage}?selectedTab={Constants.Discovery}", UriKind.Absolute));
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
    }
}
