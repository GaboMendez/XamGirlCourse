using System;
using System.Collections.Generic;
using System.Text;
using Prism.Navigation;
using Prism.Services;

namespace Homework04.ViewModels
{
    public class AddEditContactPageViewModel : ViewModelBase
    {
        public AddEditContactPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) 
            : base(navigationService, pageDialogService)
        {
        }
    }
}
