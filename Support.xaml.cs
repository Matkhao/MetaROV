namespace MetaRov;

public partial class Support : ContentPage
{
	public Support()
	{
		InitializeComponent();
	}
    private async void videoBT_Clicked(object sender, EventArgs e)
    {
        string youtubeUrl = "https://www.youtube.com/watch?v=meS8d4LXM_w";
        await Launcher.OpenAsync(youtubeUrl);
    }
}