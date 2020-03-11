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
using XF.Material.Forms.UI.Dialogs.Configurations;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;
using XF.Material.Forms.Resources;

namespace Homework05.ViewModels
{
    public class AnimeDetailPageViewModel : ViewModelBase
    {
        // Properties
        public Anime Anime { get; set; }
        public string Sypnosis { get; set; } = "";
        public string MoreSypnosis { get; set; } = "...";
        public bool TrailerBool { get; set; } = false;
        public bool InfoBool { get; set; } = false;
        public bool SummaryBool { get; set; } = false;

        // Commands
        public DelegateCommand TrailerCommand { get; set; }
        public DelegateCommand MoreCommand { get; set; }
        public DelegateCommand OpeningCommand { get; set; }
        public DelegateCommand EndingCommand { get; set; }
        public DelegateCommand InfoCommand { get; set; }

        public AnimeDetailPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) 
            : base(navigationService, pageDialogService)
        {
            TrailerCommand = new DelegateCommand(async () => { await Trailer(); });
            MoreCommand = new DelegateCommand(async () => { await More(); });
            OpeningCommand = new DelegateCommand(async () => { await Opening(); });
            EndingCommand = new DelegateCommand(async () => { await Ending(); });
            InfoCommand = new DelegateCommand(async () => { await Info(); });
        }

        private async Task Ending()
        {
            var alertDialogConfiguration = new MaterialAlertDialogConfiguration
            {
                BackgroundColor = Color.FromHex("#011a27"),
                TitleTextColor = Color.White,
                MessageTextColor = Color.White.MultiplyAlpha(0.8),
                TintColor = Color.White,
                CornerRadius = 8,
                Margin = new Thickness(5, 0, 5, 0),
                ScrimColor = Color.FromHex("#232F34").MultiplyAlpha(0.32),
                ButtonAllCaps = false,
            };
            
            string message = "";
            int number = 1;   
            
            foreach (var item in Anime.ending_themes)
                if(number < 9) 
                    message += $"{number++}) {item} \n";

            await MaterialDialog.Instance.AlertAsync(message: message,
                                                     title: "Endings List",
                                                     acknowledgementText: "Got It",
                                                     configuration: alertDialogConfiguration);
        }

        private async Task Opening()
        {
            var alertDialogConfiguration = new MaterialAlertDialogConfiguration
            {
                BackgroundColor = Color.FromHex("#011a27"),
                TitleTextColor = Color.White,
                MessageTextColor = Color.White.MultiplyAlpha(0.8),
                TintColor = Color.White,
                CornerRadius = 8,
                Margin = new Thickness(5, 0, 5, 0),
                ScrimColor = Color.FromHex("#232F34").MultiplyAlpha(0.32),
                ButtonAllCaps = false,
            };

            string message = "";
            int number = 1;

            foreach (var item in Anime.opening_themes)
                if (number < 9)
                    message += $"{number++}) {item} \n";

            await MaterialDialog.Instance.AlertAsync(message: message,
                                                     title: "Openings List",
                                                     acknowledgementText: "Got It",
                                                     configuration: alertDialogConfiguration);
        }

        private async Task More()
        {
            var alertDialogConfiguration = new MaterialAlertDialogConfiguration
            {
                BackgroundColor = Color.FromHex("#011a27"),
                TitleTextColor = Color.White,
                MessageTextColor = Color.White.MultiplyAlpha(0.8),
                TintColor = Color.White,
                CornerRadius = 8, 
                Margin = new Thickness(5,0,5,0),
                ScrimColor = Color.FromHex("#232F34").MultiplyAlpha(0.32),
                ButtonAllCaps = false,
            };

            await MaterialDialog.Instance.AlertAsync(message: MoreSypnosis,
                                                     title: "Sypnosis Continue",
                                                     acknowledgementText: "Got It",
                                                     configuration: alertDialogConfiguration);
        }
        
        private async Task Info()
        {
            try
            {
                await Browser.OpenAsync(new Uri(Anime.url), BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                await DialogService.DisplayAlertAsync("Device does not support browser navigation!", null, "Ok");
            }
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
                await DialogService.DisplayAlertAsync("Device does not support browser navigation!", null, "Ok");
            }
        }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);    
            if (parameters.ContainsKey("Anime"))
            {
                Anime = (Anime)parameters["Anime"];
                var summary = Anime.synopsis.Split('.');

                for (int i = 0; i < summary.Length - 1; i++)
                {
                    summary[i] += ".";

                    if (Sypnosis.Length + summary[i].Length > 450 || SummaryBool)
                    {
                        if (MoreSypnosis.Length + Sypnosis.Length > 800)
                            break;

                        MoreSypnosis += summary[i];
                        SummaryBool = true;
                    }
                    else
                        Sypnosis += summary[i];
                }

                if (Anime.trailer_url != null)
                    TrailerBool = true;

                if (Anime.url != null)
                    InfoBool = true;

            }
        }
    }
}
