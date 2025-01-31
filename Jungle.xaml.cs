namespace MetaRov;

public partial class Jungle : ContentPage
{
	public Jungle()
	{
		InitializeComponent();
	}

    private async void videoBT_Clicked(object sender, EventArgs e)
    {
        string youtubeUrl = "https://www.youtube.com/watch?v=0_I2ZZ__73Y";
        await Launcher.OpenAsync(youtubeUrl);
    }
}