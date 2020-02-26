using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Homework04.Models;
using Plugin.Media;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace Homework04.ViewModels
{
    public class AddEditContactPageViewModel : ViewModelBase
    {
        // Properties
        public bool Edit { get; set; } = false;
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
            PictureCommand = new DelegateCommand(async () => { await Picture(); });
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
        
        private async Task Picture()
        {
            var actionSheet = await DialogService.DisplayActionSheetAsync("Change Photo", "Cancel",
                                                                                null, "Take Photo", "Choose Photo");
            switch (actionSheet)
            {
                case "Take Photo":
                    try
                    {
                        if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                        {
                            await DialogService.DisplayAlertAsync("No Camera Available.", null, "OK");
                            return;
                        }

                        var file = await CrossMedia.Current.TakePhotoAsync(
                            new Plugin.Media.Abstractions.StoreCameraMediaOptions
                            {
                                // Variable para guardar la foto en el album público
                                SaveToAlbum = true
                            });

                        if (file == null)
                            return;

                        NewContact.Image = file.Path;
                        Image = NewContact.Image;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        await DialogService.DisplayAlertAsync("Permission Denied", "Give camera permissions to the device.", "Ok");
                    }

                    break;

                case "Choose Photo":
                    try
                    {
                        if (!CrossMedia.Current.IsPickPhotoSupported)
                        {
                            await DialogService.DisplayAlertAsync("Permission not granted to photos.", null, "OK");
                            return;
                        }
                        var file = await CrossMedia.Current.PickPhotoAsync().ConfigureAwait(true);

                        if (file == null)
                            return;

                        NewContact.Image = file.Path;
                        Image = NewContact.Image;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        await DialogService.DisplayAlertAsync("Permission Denied", "Give camera permissions to the device.", "Ok");
                    }

                    break;
            }
        }


        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            if (parameters.ContainsKey("EditContact"))
            {
                NewContact = (Contact)parameters["EditContact"];
                Image = NewContact.Image;
                Edit = true;
            }
        }
    }
}
