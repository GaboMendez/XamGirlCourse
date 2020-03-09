using Homework05.Models;
using Prism.Navigation;
using Prism.Services;
using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Homework05.ViewModels
{
    public class AnimeDetailPageViewModel : ViewModelBase
    {
        // Properties
        public Anime Anime { get; set; }

        // Commands
        public AnimeDetailPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) 
            : base(navigationService, pageDialogService)
        {

        }
       
        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            if (parameters.ContainsKey("Anime"))
            {
                Anime = (Anime)parameters["Anime"];                
            }
        }
    }
}
