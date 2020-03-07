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

            SearchCommand = new DelegateCommand(async () => { await Search(); });
            CancelCommand = new DelegateCommand( () => { Cancel(); });
        }

        private void Cancel()
        {
            CancelBool = CancelBool ? !CancelBool : !CancelBool;
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

                }
                
                CancelBool = true;
            }

        }
    }
}
