using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Homework04.Models;
using Prism.Navigation;
using Prism.Services;

namespace Homework04.ViewModels
{
    public class ContactPageViewModel : ViewModelBase
    {
        public ObservableCollection<Contact> Contacts { get; set; }
        public ContactPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) 
            : base(navigationService, pageDialogService)
        {
            Title = "Contact";
            Contacts = GetContacts();
        }

        private ObservableCollection<Contact> GetContacts()
        {
            var ret = new ObservableCollection<Contact>
            {
                new Contact(0, "ic_star", "ic_initialL", "Lady" ),
                new Contact(1, null, "ic_initialF", "Francis"),
                new Contact(2, null, "ic_initialO", "Oliver")
            };
            
            return ret;
        }
    }
}
