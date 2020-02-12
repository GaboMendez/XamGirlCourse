using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Prism.Services;

namespace Homework02.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) 
            : base(navigationService, pageDialogService)
        {
            Title = "Main Page";
        }
    }
}
