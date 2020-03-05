using System;
using System.Collections.Generic;
using System.Text;
using Homework03.Models;
using Prism.Navigation;
using Prism.Services;

namespace Homework03.ViewModels
{
    public class DiscoveryPageViewModel : ViewModelBase
    {
        public DiscoveryPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) 
            : base(navigationService, pageDialogService)
        { }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            if (parameters.ContainsKey("User"))
            {
                User = (User) parameters["User"];
            }
        }
    }
}
