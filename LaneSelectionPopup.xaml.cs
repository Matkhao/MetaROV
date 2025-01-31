using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls;
using System;

namespace MetaRov
{
    public partial class LaneSelectionPopup : Popup
    {
        private string selectedLane;

        public LaneSelectionPopup()
        {
            InitializeComponent();
        }

        private void OnLaneSelected(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                selectedLane = button.Text;
            }
        }

        private void OnConfirmClicked(object sender, EventArgs e)
        {
            Close(selectedLane);
        }

        private async void fighterBT_Clicked(object sender, EventArgs e)
        {
            Close();
            await Application.Current.MainPage.Navigation.PushAsync(new Offlane());
        }

        private async void mageBT_Clicked(object sender, EventArgs e)
        {
            Close();
            await Application.Current.MainPage.Navigation.PushAsync(new Mage());
        }

        private async void carryBT_Clicked(object sender, EventArgs e)
        {
            Close();
            await Application.Current.MainPage.Navigation.PushAsync(new Carry());
        }

        private async void jungleBT_Clicked(object sender, EventArgs e)
        {
            Close();
            await Application.Current.MainPage.Navigation.PushAsync(new Jungle());
        }

        private async void supportBT_Clicked(object sender, EventArgs e)
        {
            Close();
            await Application.Current.MainPage.Navigation.PushAsync(new Support());
        }
    }
}
