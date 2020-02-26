using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Homework04.Models;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace Homework04.ViewModels
{
    public class AddEditContactPageViewModel : ViewModelBase
    {
        // Properties
        public string Image { get; set; }
        public Contact NewContact { get; set; }

        // Commands
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand PictureCommand { get; set; }

        public AddEditContactPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) 
            : base(navigationService, pageDialogService)
        {
            NewContact = new Contact();
            Title = "Create Contact";
            Image = "ic_picture";

            SaveCommand = new DelegateCommand(async () => { await Save(); });
        }

        private async Task Save()
        {
            if (String.IsNullOrEmpty(NewContact.FirstName) || String.IsNullOrEmpty(NewContact.LastName) || String.IsNullOrEmpty(NewContact.Company) || 
                String.IsNullOrEmpty(NewContact.Phone) || String.IsNullOrEmpty(NewContact.TypePhone) || String.IsNullOrEmpty(NewContact.Email) || String.IsNullOrEmpty(NewContact.TypeEmail))
            {
                await DialogService.DisplayAlertAsync("Fields can not be empty! \nTry again!", null, "Ok");
            }
            else
            {
                NewContact.Image = Image.Equals("ic_picture") ? "ic_default" : Image;
                NewContact.Category = "ic_common";

                var contactParameters = new NavigationParameters
                {
                    { "Contact", NewContact },
                    { "New", true }
                };

                await NavigationService.GoBackAsync(contactParameters);
            }
        }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            if (parameters.ContainsKey("EditContact"))
            {
                NewContact = (Contact)parameters["EditContact"];
            }
        }
    }
}
