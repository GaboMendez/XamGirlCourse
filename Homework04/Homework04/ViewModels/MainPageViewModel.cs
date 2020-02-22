using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Prism.Services;

namespace Homework04.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public int N { get; set; } = 0;

        public DelegateCommand buttCommand { get; set; }
        public MainPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
            : base(navigationService, pageDialogService)
        {
            Title = "Main Page";

            buttCommand = new DelegateCommand(() =>
            {
                List<string> val = new List<string>
                {
                    "Prueba 01",
                    "Prueba 02",
                    "Prueba 03"
                };

                Title = val[N];
                N++;

                if (N.Equals(val.Count))
                {
                    N = 0;
                }
            });
        }
    }
}
