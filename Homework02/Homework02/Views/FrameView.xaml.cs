using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Homework02.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FrameView : ContentView
    {
        public bool Selected { get; set; } = false;
        public string Image
        {
            set => image.Source = value;
        }
        public string Title
        {
            set => title.Text = value;
        } 
        public string Text
        {
            set => text.Text = value;
        }

        public FrameView()
        {
            InitializeComponent();
        }

        private void Frame_OnTapped(object sender, EventArgs e)
        {
            if (Selected)
            {
                Selected = false;

                text.TextColor = Color.DarkGray;
                frame.BackgroundColor = Color.FromHex("FFFFFF");
                frame.Margin = new Thickness(0, 0, 0, 0);
            }
            else
            {
                Selected = true;

                text.TextColor = Color.Black;
                frame.BackgroundColor = Color.FromHex("FFCC2A");
                frame.Margin = new Thickness(0, -5, 0, 5);
            }
        }
    }
}