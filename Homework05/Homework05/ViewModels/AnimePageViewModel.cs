using Homework05.Models;
using Homework05.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs.Configurations;
using XF.Material.Forms.UI.Dialogs;

namespace Homework05.ViewModels
{
    public class AnimePageViewModel : ViewModelBase
    {
        // Properties
        protected IApiService ApiService { get; set; }
        protected int Page;
        public string SearchText { get; set; } = "";
        public bool CancelBool { get; set; } = false;

        private ObservableCollection<Top> _observableAnimeList;
        public ObservableCollection<Top> ObservableAnimeList
        {
            get { return _observableAnimeList; }
            set { _observableAnimeList = value; }
        }

        private bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set { _isRefreshing = value; }
        }
        
        // Commands
        public DelegateCommand SearchCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }
        public DelegateCommand RefreshCommand { get; set; }
        public AnimePageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) 
            : base(navigationService, pageDialogService)
        {
            Title = "AnimeX";
            ApiService = new ApiService();
            ObservableAnimeList = new ObservableCollection<Top>();

            RefreshCommand = new DelegateCommand(async () => { await Refresh(); });
            SearchCommand = new DelegateCommand(async () => { await Search(); });
            CancelCommand = new DelegateCommand(async () => { await Cancel(); });
        }

        private async Task Refresh()
        {
            IsRefreshing = true;

            if (CancelBool)
                CancelBool = !CancelBool;
            SearchText = "";
            var ret = await ApiService.GetTopAnimes(Interlocked.Increment(ref Page));           
            List<Top> AnimeList = ret.top.Where(x => x.type.Equals("TV")).ToList();
            ObservableAnimeList = ToObservable(AnimeList);

            IsRefreshing = false;
        }

        private async Task Cancel()
        {
            CancelBool = !CancelBool;
            SearchText = "";

            var loadingDialogConfiguration = new MaterialLoadingDialogConfiguration()
            {
                BackgroundColor = Color.FromHex("#011a27"),
                MessageTextColor = Color.White.MultiplyAlpha(0.8),
                TintColor = Color.White,
                CornerRadius = 8,
                ScrimColor = Color.FromHex("#232F34").MultiplyAlpha(0.32)
            };

            using (await MaterialDialog.Instance.LoadingDialogAsync(message: "Loading Animes...",
                                                                    configuration: loadingDialogConfiguration))
            {
                var ret = await ApiService.GetTopAnimes(Interlocked.Increment(ref Page));
                List<Top> AnimeList = ret.top.Where(x => x.type.Equals("TV")).ToList();
                ObservableAnimeList = ToObservable(AnimeList);
            }
          
        }

        private async Task Search()
        {

            if (string.IsNullOrEmpty(SearchText))
            {
                await DialogService.DisplayAlertAsync("Fields can not be empty! \nTry again!", null, "Ok");
            }
            else
            {
                var loadingDialogConfiguration = new MaterialLoadingDialogConfiguration()
                {
                    BackgroundColor = Color.FromHex("#011a27"),
                    MessageTextColor = Color.White.MultiplyAlpha(0.8),
                    TintColor = Color.White,
                    CornerRadius = 8,
                    ScrimColor = Color.FromHex("#232F34").MultiplyAlpha(0.32)
                };

                using (await MaterialDialog.Instance.LoadingDialogAsync(message: "Loading Animes...",
                                                                        configuration: loadingDialogConfiguration))
                {
                    var ret = await ApiService.SearchAnime(SearchText);
                    if (ret.results.Count > 3)
                    {
                        List<Top> AnimeList = ret.results.Where(x => x.type.Equals("TV")).ToList();
                        ObservableAnimeList = ToObservable(AnimeList);
                        CancelBool = true;
                    }
                    else
                    {
                        ObservableAnimeList = new ObservableCollection<Top>();
                        CancelBool = true;
                    }
                }
                

            }
        }
        private ObservableCollection<Top> ToObservable(List<Top> enumerable)
        {
            var ret = new ObservableCollection<Top>();
            foreach (var cur in enumerable)
            {
                ret.Add(cur);
            }
            return ret;
        }
        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            if (parameters.ContainsKey("Animes"))
            {
                ObservableAnimeList = ToObservable( (List<Top>)parameters["Animes"] );
                Page = 1;
            }
        }

    }
}
