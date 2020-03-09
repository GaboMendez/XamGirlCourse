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
        public bool IsRefreshing { get; set; } = false;
        private Top _selectedAnime;
        public Top SelectedAnime
        {
            get { return _selectedAnime; }
            set
            {
                if (_selectedAnime != value)
                {
                    _selectedAnime = value;
                    _ = DetailAnime(_selectedAnime);
                }
            }
        }
        public ObservableCollection<Top> ObservableAnime { get; set; }

        // Commands
        public DelegateCommand SearchCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }
        public DelegateCommand RefreshCommand { get; set; }
        public DelegateCommand NextPageCommand { get; set; }
        public DelegateCommand BackPageCommand { get; set; }

        public AnimePageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) 
            : base(navigationService, pageDialogService)
        {
            Title = "AnimeX";
            ApiService = new ApiService();
            ObservableAnime = new ObservableCollection<Top>();

            RefreshCommand = new DelegateCommand(async () => { await Refresh(); });
            SearchCommand = new DelegateCommand(async () => { await Search(); });
            CancelCommand = new DelegateCommand(async () => { await Cancel(); });
            NextPageCommand = new DelegateCommand(async () => { await NextPage(); });
            BackPageCommand = new DelegateCommand(async () => { await BackPage(); });
        }

        private async Task DetailAnime(Top selectedAnime)
        {
            if (!IsNotConnected)
            {
                var loadingDialogConfiguration = new MaterialLoadingDialogConfiguration()
                {
                    BackgroundColor = Color.FromHex("#011a27"),
                    MessageTextColor = Color.White.MultiplyAlpha(0.8),
                    TintColor = Color.White,
                    CornerRadius = 8,
                    ScrimColor = Color.FromHex("#232F34").MultiplyAlpha(0.32)
                };

                using (await MaterialDialog.Instance.LoadingDialogAsync(message: "Loading Anime...",
                                                                        configuration: loadingDialogConfiguration))
                {
                    var anime = await ApiService.SearchAnimeID(selectedAnime.mal_id);
                    await NavigationService.NavigateAsync(new Uri($"/{Constants.AnimeDetail}", UriKind.Relative), ("Anime", anime));
                }
            }
            else
                await DialogService.DisplayAlertAsync("Check your internet Connection and Try Again!", null, "Ok");
        }

        private async Task BackPage()
        {
            if (Page.Equals(1))
                return;

            if (!IsNotConnected)
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
                    var ret = await ApiService.GetTopAnimes(Interlocked.Decrement(ref Page));
                    List<Top> AnimeList = ret.top.Where(x => x.type.Equals("TV")).ToList();
                    ObservableAnime = ToObservable(AnimeList);
                }
            }
            else
                await DialogService.DisplayAlertAsync("Check your internet Connection and Try Again!", null, "Ok");
        }

        private async Task NextPage()
        {
            if (!IsNotConnected)
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
                    var ret = await ApiService.GetTopAnimes(Interlocked.Increment(ref Page));
                    List<Top> AnimeList = ret.top.Where(x => x.type.Equals("TV")).ToList();
                    ObservableAnime = ToObservable(AnimeList);
                }
            }
            else
                await DialogService.DisplayAlertAsync("Check your internet Connection and Try Again!", null, "Ok");            
        }

        private async Task Refresh()
        {
            if (!IsNotConnected)
            {
                IsRefreshing = true;

                SearchText = "";
                if (CancelBool)
                    CancelBool = !CancelBool;

                var ret = await ApiService.GetTopAnimes(Interlocked.Increment(ref Page));
                List<Top> AnimeList = ret.top.Where(x => x.type.Equals("TV")).ToList();
                ObservableAnime = ToObservable(AnimeList);

                IsRefreshing = false;
            }
            else
                await DialogService.DisplayAlertAsync("Check your internet Connection and Try Again!", null, "Ok");           
        }

        private async Task Cancel()
        {
            if (!IsNotConnected)
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
                    ObservableAnime = ToObservable(AnimeList);
                }

            }
            else
                await DialogService.DisplayAlertAsync("Check your internet Connection and Try Again!", null, "Ok");         
        }

        private async Task Search()
        {
            if (!IsNotConnected)
            {
                if (string.IsNullOrEmpty(SearchText))
                    await DialogService.DisplayAlertAsync("Fields can not be empty! \nTry again!", null, "Ok");
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
                        List<Top> AnimeList = ret.results.Where(x => x.type.Equals("TV")).ToList();

                        if (AnimeList.Count > 1)
                            ObservableAnime = ToObservable(AnimeList);
                        else
                            ObservableAnime = new ObservableCollection<Top>();

                        CancelBool = true;
                    }
                }
            }
            else
                await DialogService.DisplayAlertAsync("Check your internet Connection and Try Again!", null, "Ok");
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

            if (parameters.ContainsKey("AnimeList"))
            {
                ObservableAnime = ToObservable( (List<Top>)parameters["AnimeList"] );
                Page = 1;
            }
        }

    }
}
