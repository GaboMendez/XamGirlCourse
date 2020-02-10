using System;
using System.Collections.Generic;
using System.Text;
using Prism.Navigation;

namespace Extra01.ViewModels
{
    public class OtherPageViewModel : ViewModelBase
    {
        public OtherPageViewModel(INavigationService navigationService) 
            : base(navigationService)
        {
        }
    }
}
