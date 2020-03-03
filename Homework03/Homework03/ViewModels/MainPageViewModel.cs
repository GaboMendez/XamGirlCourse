using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Prism.Services;

namespace Homework03.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public DelegateCommand LogoutCommand { get; set; }
        public MainPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) 
            : base(navigationService, pageDialogService)
        {
            Title = "Title";

            LogoutCommand = new DelegateCommand(async () =>
            {
                var answer = await DialogService.DisplayAlertAsync("Are you sure you want to \nlog out?", null, "Yes", "No");
                if (answer)
                {
                    await NavigationService.NavigateAsync(new Uri($"/{Constants.Navigation}/{Constants.Login}", UriKind.Absolute));
                }
            });
        }
    }
}
