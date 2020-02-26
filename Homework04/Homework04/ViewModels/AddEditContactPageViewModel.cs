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
        public Contact Contact { get; set; }

        // Commands
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand PictureCommand { get; set; }

        public AddEditContactPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) 
            : base(navigationService, pageDialogService)
        {
            Title = "Create Contact";
            Image = "ic_picture";

            SaveCommand = new DelegateCommand(async () => { await Save(); });
            PictureCommand = new DelegateCommand(async () => { await Picture(); });
        }

        private async Task Save()
        {
            if (String.IsNullOrEmpty(Contact.FirstName) || String.IsNullOrEmpty(Contact.LastName) || String.IsNullOrEmpty(Contact.Company) || 
                String.IsNullOrEmpty(Contact.Phone) || String.IsNullOrEmpty(Contact.TypePhone) || String.IsNullOrEmpty(Contact.Email) || String.IsNullOrEmpty(Contact.TypeEmail))
            {
                await DialogService.DisplayAlertAsync("Fields can not be empty! \nTry again!", null, "Ok");
            }
            else
            {
                if (Edit)
                {
                    Contact.Image = Image.Equals("ic_picture") ? "ic_default" : Image;

                    var contactParameters = new NavigationParameters
                    {
                        { "Contact", Contact },
                        { "New", false }
                    };

                    await NavigationService.GoBackAsync(contactParameters);
                }
                else
                {
                    Contact.Image = Image.Equals("ic_picture") ? "ic_default" : Image;
                    Contact.Category = "ic_common";

                    var contactParameters = new NavigationParameters
                    {
                        { "Contact", Contact },
                        { "New", true }
                    };

                    await NavigationService.GoBackAsync(contactParameters);
                }
               
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

                        Contact.Image = file.Path;
                        Image = Contact.Image;
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

                        Contact.Image = file.Path;
                        Image = Contact.Image;
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
                Title = "Update Contact";
                Contact = (Contact)parameters["EditContact"];
                Image = Contact.Image;
                Edit = true;
            }else
                Contact = new Contact();

        }
    }
}
