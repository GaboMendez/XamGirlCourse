using System;
using System.Collections.Generic;
using System.Text;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace Homework02.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        public DelegateCommand SignupCommand { get; set; }

        public LoginPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) 
            : base(navigationService, pageDialogService)
        {

            SignupCommand = new DelegateCommand(async () =>
            {
                Console.WriteLine();
                //await NavigationService.NavigateAsync(new Uri($"/{Constants.Signup}", UriKind.Relative));
            });
        }
    }
}
