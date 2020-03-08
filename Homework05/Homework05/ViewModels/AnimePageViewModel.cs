using Homework05.Models;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Homework05.ViewModels
{
    public class AnimePageViewModel : ViewModelBase
    {
        // Properties
        public string SearchText { get; set; } = "";
        public bool CancelBool { get; set; } = false;

        private ObservableCollection<Post> _observablePosts;
        public ObservableCollection<Post> ObservablePosts
        {

            get { return _observablePosts; }
            private set
            {
                _observablePosts = value;
                //RaisePropertyChanged("ObservableContacts");
            }
        }

        private bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                //RaisePropertyChanged("IsRefreshing");
            }
        }


        // Commands
        public DelegateCommand SearchCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }
        public AnimePageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) 
            : base(navigationService, pageDialogService)
        {
            Title = "AnimeX";
            ObservablePosts = new ObservableCollection<Post>();
            SearchCommand = new DelegateCommand(async () => { await Search(); });
            CancelCommand = new DelegateCommand( () => { Cancel(); });
        }

        private void Cancel()
        {
            CancelBool = !CancelBool;
            SearchText = "";
        }

        private async Task Search()
        {
            if (string.IsNullOrEmpty(SearchText))
            {
                await DialogService.DisplayAlertAsync("Fields can not be empty! \nTry again!", null, "Ok");
            }
            else
            {
                if (ObservablePosts.Count.Equals(0))
                {
                    ObservablePosts = new ObservableCollection<Post>
                    { 
                        new Post("Fullmetal Alchemist: Brotherhood","https://cdn.myanimelist.net/images/anime/1223/96541.jpg?s=faffcb677a5eacd17bf761edd78bfb3f"),
                        new Post("Gintama","https://cdn.myanimelist.net/images/anime/3/72078.jpg?s=e9537ac90c08758594c787ede117f209"),
                        new Post("Hunter x Hunter (2011)","https://cdn.myanimelist.net/images/anime/11/33657.jpg?s=5724d8c22ae7a1dad72d8f4229ef803f"),
                        new Post("Shingeki no Kyojin Season 3 Part 2","https://cdn.myanimelist.net/images/anime/1517/100633.jpg?s=4540a01b5883647ade494cd28392f100"),
                    };
                }
                CancelBool = true;
            }

        }
    }
}
