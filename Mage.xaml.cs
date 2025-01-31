using Microsoft.Maui.Controls;

namespace MetaRov
{
    public partial class Mage : ContentPage
    {
        public Mage()
        {
            InitializeComponent();
        }

        private async void midlaneBT_Clicked(object sender, EventArgs e)
        {
            string youtubeUrl = "https://www.youtube.com/watch?v=nCpyI4d75ME";
            await Launcher.OpenAsync(youtubeUrl);
        }
    }
}
