using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls;

namespace MetaRov;
public partial class HeroPopup : Popup
{
    public HeroPopup(string heroName, string imagePath, string description)
    {
        InitializeComponent();
        HeroImage.Source = imagePath; // ✅ กำหนดภาพฮีโร่
        HeroDescription.Text = $"จากคำตอบของคุณ\nพบว่าคุณมีลักษณะนิสัยใกล้เคียงกับ {heroName}"; // ✅ กำหนดข้อความ
    }

    private void ClosePopup_Clicked(object sender, EventArgs e)
    {
        Close(); // ✅ ปิด Popup
        Application.Current.MainPage = new NavigationPage(new MainPage());
    }
}
