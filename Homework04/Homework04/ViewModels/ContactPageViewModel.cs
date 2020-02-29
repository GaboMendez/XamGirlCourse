using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Homework04.Models;
using MonkeyCache.FileStore;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

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
        public DelegateCommand QRCommand { get; set; }
        public DelegateCommand<Contact> RemoveCommand { get; set; }
        public DelegateCommand<Contact> MoreCommand { get; set; }

        public ContactPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) 
            : base(navigationService, pageDialogService)
        {
            Title = "Contact";
            Contacts = GetContacts();
            //Barrel.ApplicationId = "AllUsers";

            AddCommand = new DelegateCommand(async () => { await Add(); });
            QRCommand = new DelegateCommand(async () => { await CodeQR(); });
            RemoveCommand = new DelegateCommand<Contact>(async (contact) => { await Remove(contact); });
            MoreCommand = new DelegateCommand<Contact>(async (contact) => { await More(contact); });
        }

     
        private ObservableCollection<Contact> GetContacts()
        {
            var ret = new ObservableCollection<Contact>
            {
                new Contact("ic_star", "ic_initialA", "Abril", "Martinez", "Claro", "809-312-4578", "Mobile", "abril@hotmail.com", "Personal" ),
                new Contact(null, "ic_initialF", "Francis", "Mendez", "INTEC", "809-456-7895", "Home", "francis@hotmail.com", "Work"),
                new Contact(null, "ic_initialH", "Hecmanuel", "Taveraz", "KFC", "809-789-4562", "Work", "hecmanuel@hotmail.com", "Other"),
                new Contact("ic_fontA", "ic_initialA", "Anabelle", "Herrera", "La Cadena", "829-456-8978", "Mobile", "anabelle@gmail.com", "Personal"),
                new Contact(null, "ic_initialA", "Ana Maria", "Mercedez", "PiTech", "829-456-7812", "Mobile", "ana@hotmail.com", "Work"),
                new Contact(null, "ic_initialA", "Abril", "Aquino", "Bravo", "809-456-8978", "Home", "abril@hotmail.com", "Personal"),
                new Contact(null, "ic_initialA", "Adriana", "Ruiz", "Altice", "809-123-5689", "Other", "adriana@hotmail.com", "Personal"),
                new Contact(null, "ic_initialA", "Alberto", "Morillo", "Viva", "829-567-1236", "Mobile", "alberto@gmail.com", "Work")
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
        private async Task CodeQR()
        {
            try
            {
                var ScannerPage = new ZXingScannerPage();
                await App.Current.MainPage.Navigation.PushAsync(ScannerPage);

                ScannerPage.OnScanResult += (result) =>
                {
                    ScannerPage.IsScanning = false;

                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await App.Current.MainPage.Navigation.PopAsync();
                        var NewContact = result.Text.Split('*');
                        Contacts.Add(new Contact(NewContact[0], NewContact[1], NewContact[2], "ic_common", "ic_default"));
                        await DialogService.DisplayAlertAsync($"{NewContact[0]} {NewContact[1]}", "Your contact has been \nsuccessfully created", "Ok");
                    });
                };


            }
            catch (Exception ex)
            {
                await DialogService.DisplayAlertAsync("Error!", ex.Message, "Ok");
            }
          
        }

        private async Task Remove(Contact contact)
        {
            bool wantsDelete = await DialogService.DisplayAlertAsync("Are you sure you want \nto delete this Contact?", null, "Yes", "No");
            if (wantsDelete)
            {
                Contacts.Remove(contact);
            }
        }

        private async Task More(Contact contact)
        {
            var option01 = $"Call +{contact.Phone}";
            var option02 = "Update";

            var actionSheet = await DialogService.DisplayActionSheetAsync("Select an Option", "Cancel", 
                                                                                null, option01, option02);

            if (actionSheet.Equals(option01))
            {
                try { PhoneDialer.Open(contact.Phone); }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    await DialogService.DisplayAlertAsync("Phone Dialer is not supported on this device.", null, "Ok");
                }
            }
            if (actionSheet.Equals(option02))
            {
                var contactParameters = new NavigationParameters
                {
                    { "EditContact", contact }
                };
                await NavigationService.NavigateAsync(new Uri($"/{Constants.AddOrEdit}", UriKind.Relative), contactParameters);
            }
        }

        private async Task UpdateList(int userID, ObservableCollection<Contact> contacts)
        {
            await Task.Delay(100);

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (parameters.ContainsKey("Contact") && parameters.ContainsKey("New"))
            {
                var newContact = (bool) parameters["New"];
                var contact = (Contact) parameters["Contact"];

                if (newContact)
                    Contacts.Add(contact);
                else
                    Contacts[contact.ID] = contact;

            }

            if (parameters.ContainsKey("User"))
                User = (User)parameters["User"];

        }

    }
}
