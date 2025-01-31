using System;
using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls;

namespace MetaRov
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            var titleGrid = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }, 
                    new ColumnDefinition { Width = GridLength.Auto }, 
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) } 
                },
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Center
            };

            var titleLabel = new Label
            {
                Text = "Home",
                FontSize = 20,
                HorizontalTextAlignment = TextAlignment.Center, 
                VerticalTextAlignment = TextAlignment.Center,
                TextColor = Color.FromArgb("#a09e9f")
            };

            var profileImage = new Image
            {
                Source = "rov_logo.png",
                WidthRequest = 40,
                HeightRequest = 40,
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center,
                Margin = new Thickness(10, 0, 10, 0) 
            };

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += OnProfileImageTapped;
            profileImage.GestureRecognizers.Add(tapGestureRecognizer);

            titleGrid.Children.Add(titleLabel);
            Grid.SetColumn(titleLabel, 1);

            titleGrid.Children.Add(profileImage);
            Grid.SetColumn(profileImage, 2);

            NavigationPage.SetTitleView(this, titleGrid);
        }
        private async void OnProfileImageTapped(object sender, EventArgs e)
        {
            string rovAppUriAndroid = "intent://com.garena.game.kgth#Intent;scheme=package;end"; // เปิดแอป ROV บน Android
            string rovAppURIIOS = "arenaofvalor://"; // เปิดแอป ROV บน iOS

            string rovPlayStoreUrl = "https://play.google.com/store/apps/details?id=com.garena.game.kgth"; // Play Store
            string rovAppStoreUrl = "https://apps.apple.com/th/app/arena-of-valor/id1150318642"; // App Store

            try
            {
                if (DeviceInfo.Platform == DevicePlatform.Android)
                {
                    bool isAppOpened = await Launcher.TryOpenAsync(rovAppUriAndroid);
                    if (!isAppOpened)
                    {
                        // ถ้าไม่มีแอปให้เปิด Play Store
                        await Launcher.OpenAsync(rovPlayStoreUrl);
                    }
                }
                else if (DeviceInfo.Platform == DevicePlatform.iOS)
                {
                    bool isAppOpened = await Launcher.TryOpenAsync(rovAppURIIOS);
                    if (!isAppOpened)
                    {
                        // ถ้าไม่มีแอปให้เปิด App Store
                        await Launcher.OpenAsync(rovAppStoreUrl);
                    }
                }
                else
                {
                    await DisplayAlert("ไม่รองรับ", "แพลตฟอร์มนี้ไม่รองรับการเปิดแอป ROV", "ตกลง");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("เกิดข้อผิดพลาด", ex.Message, "ตกลง");
            }
        }

        private async void tierlistBT_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Tierlist());
        }

        private async void quizBT_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Quiz());
        }

        private async void ShowLaneSelectionPopup(object sender, EventArgs e)
        {
            var popup = new LaneSelectionPopup();
            var result = await this.ShowPopupAsync(popup);

            if (result != null)
            {
                await DisplayAlert("เลือกตำแหน่ง", $"คุณเลือก: {result}", "ตกลง");
            }
        }

    }

}
