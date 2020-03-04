using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Prism.Services;
using System.Threading.Tasks;
using System.Globalization;
using System.Threading;

namespace Homework03.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        // Properties
        private string _selectedValue;
        public string SelectedValue
        {
            get { return _selectedValue; }
            set
            {
                if (_selectedValue != value)
                {
                    _selectedValue = value;

                    switch (_selectedValue)
                    {
                        case "Spanish":
                            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-ES");
                            _ = NavigationService.NavigateAsync(new Uri($"/{Constants.Navigation}/{Constants.TabbedPage}?selectedTab={Constants.Discovery}", UriKind.Absolute));
                            break;

                        case "English":
                            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                            _ = NavigationService.NavigateAsync(new Uri($"/{Constants.Navigation}/{Constants.TabbedPage}?selectedTab={Constants.Discovery}", UriKind.Absolute));
                            break;
                    }                  
                }
            }
        }
        // Commands
        public DelegateCommand LogoutCommand { get; set; }

        public MainPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) 
            : base(navigationService, pageDialogService)
        {
            Title = "Title";

            LogoutCommand = new DelegateCommand(async () => { await Logout(); });
          
        }

        private async Task Logout()
        {
            var answer = await DialogService.DisplayAlertAsync("Are you sure you want to \nlog out?", null, "Yes", "No");
            if (answer)
            {
                await NavigationService.NavigateAsync(new Uri($"/{Constants.Navigation}/{Constants.Login}", UriKind.Absolute));
            }
        }
    }
}
