using System;
using System.Collections.Generic;
using System.Text;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace Homework01.ViewModels
{
    public class SignUpPageViewModel : ViewModelBase
    {

        public DelegateCommand LoginCommand { get; set; }

        public SignUpPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) 
            : base(navigationService, pageDialogService)
        {

            LoginCommand = new DelegateCommand(async () =>
            {
                await NavigationService.NavigateAsync(new Uri($"/{Constants.Login}", UriKind.Relative));
            });
        }
    }
}
