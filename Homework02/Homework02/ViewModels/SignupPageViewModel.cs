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
    public class SignupPageViewModel : ViewModelBase
    {
        // Properties
        public User NewUser { get; set; }
        public bool CanExecute { get; set; } = true;
        public bool BoolPassword { get; set; } = true;
        public bool BoolConfirmPassword { get; set; } = true;

        // Commands
        public DelegateCommand SignUpCommand { get; set; }
        public DelegateCommand LoginCommand { get; set; }
        public DelegateCommand ShowPasswordCommand { get; set; }
        public DelegateCommand ShowConfirmPasswordCommand { get; set; }

        public SignupPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) 
            : base(navigationService, pageDialogService)
        {
            NewUser = new User();
            SignUpCommand = new DelegateCommand(async () =>
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
                            // Navigate to Home
                            NewUser.CreateUser(NewUser.Email, NewUser.Username, NewUser.Password);

                            await Task.Delay(400);
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

            ShowPasswordCommand = new DelegateCommand(() => { BoolPassword = !BoolPassword; });

            ShowConfirmPasswordCommand = new DelegateCommand(() => { BoolConfirmPassword = !BoolConfirmPassword; });
        }
    }
}
