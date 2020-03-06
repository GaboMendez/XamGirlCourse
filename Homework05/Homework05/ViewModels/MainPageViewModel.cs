using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Homework05.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        // Properties

        // Commands
        public DelegateCommand Command { get; set; }
        public MainPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
            : base(navigationService, pageDialogService)
        {
            Title = "Main Page";

            Command = new DelegateCommand(() => { Title = "xxxx"; });
        }
    }
}
