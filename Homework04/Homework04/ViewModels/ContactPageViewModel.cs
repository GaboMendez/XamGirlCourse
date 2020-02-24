using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Homework04.Models;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace Homework04.ViewModels
{
    public class ContactPageViewModel : ViewModelBase
    {
        // Properties
        public bool CanExecute { get; set; } = true;
        public ObservableCollection<Contact> Contacts { get; set; }
        private Contact _selectedContact;
        public Contact selectedContact
        {
            get { return _selectedContact; }
            set
            {
                _selectedContact = value;
                if (_selectedContact != null)
                {
                    _ = ContactDetails(_selectedContact);
                }
            }
        }

        // Commands
        public DelegateCommand AddCommand { get; set; }

 

        public ContactPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) 
            : base(navigationService, pageDialogService)
        {
            Title = "Contact";
            Contacts = GetContacts();

            AddCommand = new DelegateCommand(async () => { await Add(); });
        }

      

        private ObservableCollection<Contact> GetContacts()
        {
            var ret = new ObservableCollection<Contact>
            {
                new Contact(0, "ic_star", "ic_initialA", "Abril", "Martinez", "Claro", "809-312-4578", "Mobile", "abril@hotmail.com", "Personal" ),
                new Contact(1, null, "ic_initialF", "Francis", "Mendez", "INTEC", "809-456-7895", "Home", "francis@hotmail.com", "Work"),
                new Contact(2, null, "ic_initialH", "Hecmanuel", "Taveraz", "KFC", "809-789-4562", "Work", "hecmanuel@hotmail.com", "Other"),
                new Contact(3, "ic_fontA", "ic_initialA", "Anabelle", "Herrera", "La Cadena", "829-456-8978", "Mobile", "anabelle@gmail.com", "Personal"),
                new Contact(4, null, "ic_initialA", "Ana Maria", "Mercedez", "PiTech", "829-456-7812", "Mobile", "ana@hotmail.com", "Work"),
                new Contact(5, null, "ic_initialA", "Abril", "Aquino", "Bravo", "809-456-8978", "Home", "abril@hotmail.com", "Personal"),
                new Contact(6, null, "ic_initialA", "Adriana", "Ruiz", "Altice", "809-123-5689", "Other", "adriana@hotmail.com", "Personal"),
                new Contact(7, null, "ic_initialA", "Alberto", "Morillo", "Viva", "829-567-1236", "Mobile", "alberto@gmail.com", "Work")
            };

            return ret;
        }

        private async Task ContactDetails(Contact contact)
        {
            string details = $"Company: {contact.Company} \n \n" +
                             $"Phone: {contact.Phone} \n   Type: {contact.TypePhone} \n \n" +
                             $"Email: {contact.Email} \n Type: {contact.TypeEmail} ";

            await DialogService.DisplayAlertAsync(selectedContact.FullName, details, "Ok");
        }

        private async Task Add()
        {
            if (CanExecute)
            {
                CanExecute = false;

                await NavigationService.NavigateAsync(new Uri($"/{Constants.AddOrEdit}", UriKind.Relative));

                CanExecute = true;
            }
        }
    }
}
