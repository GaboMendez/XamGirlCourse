using Homework04.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Homework04.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged, IInitialize, INavigationAware, IDestructible
    {
        public static User User { get; set; }
        protected INavigationService NavigationService { get; private set; }
        protected IPageDialogService DialogService { get; private set; }
        public string Title { get; set; }

        public ViewModelBase(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            NavigationService = navigationService;
            DialogService = pageDialogService;
        }

        public virtual void Initialize(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        public virtual void Destroy()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
