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
    }
}