using Homework05.Models;
using Prism.Navigation;
using Prism.Services;
using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using Xamarin.Essentials;

namespace Homework05.ViewModels
{
    public class AnimeDetailPageViewModel : ViewModelBase
    {
        // Properties
        public Anime Anime { get; set; }
        public bool TrailerBool { get; set; } = false;

        // Commands
        public DelegateCommand TrailerCommand { get; set; }

        public AnimeDetailPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) 
            : base(navigationService, pageDialogService)
        {
            TrailerCommand = new DelegateCommand(async () => { await Trailer(); });
        }

        private async Task Trailer()
        {
            try
            {
                await Browser.OpenAsync(new Uri(Anime.trailer_url), BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                await App.Current.MainPage.DisplayAlert("Device does not support browser navigation!", null, "Ok");
            }
        }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            if (parameters.ContainsKey("Anime"))
            {
                Anime = (Anime)parameters["Anime"];
                if (Anime.trailer_url != null)
                    TrailerBool = true;
            }
        }
    }
}
