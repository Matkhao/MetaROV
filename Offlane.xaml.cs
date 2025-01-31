namespace MetaRov;

public partial class Offlane : ContentPage
{
	public Offlane()
	{
		InitializeComponent();
	}

    private async void videoBT_Clicked(object sender, EventArgs e)
    {
        string youtubeUrl = "https://www.youtube.com/watch?v=9KQov9-Ncy0";
        await Launcher.OpenAsync(youtubeUrl);
    }
}