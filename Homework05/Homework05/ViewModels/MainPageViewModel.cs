using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;
using XF.Material.Forms.UI.Dialogs.Configurations;

namespace Homework05.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        // Properties

        // Commands
        public DelegateCommand AnimeCommand { get; set; }
        public MainPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
            : base(navigationService, pageDialogService)
        {
            Title = "Main Page";

            AnimeCommand = new DelegateCommand(async () => { await Anime(); });
        }

        private async Task Anime()
        {
            var loadingDialogConfiguration = new MaterialLoadingDialogConfiguration()
            {
                BackgroundColor = Color.FromHex("#011a27"),
                MessageTextColor = Color.White.MultiplyAlpha(0.8),
                TintColor = Color.White,
                CornerRadius = 8,
                ScrimColor = Color.FromHex("#232F34").MultiplyAlpha(0.32)
            };

            using (await MaterialDialog.Instance.LoadingDialogAsync(message: "Loading Animes..",
                                                                    configuration: loadingDialogConfiguration))
            {
                await Task.Delay(1000);
                await NavigationService.NavigateAsync(new Uri($"/{Constants.Anime}", UriKind.Relative));
            }         

        }
    }
}
