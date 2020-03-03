using System;
using System.Collections.Generic;
using System.Text;
using Prism.Navigation;
using Prism.Services;

namespace Homework03.ViewModels
{
    public class DiscoveryPageViewModel : ViewModelBase
    {
        public DiscoveryPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) 
            : base(navigationService, pageDialogService)
        {
            Title = "Discovery";
        }
    }
}
