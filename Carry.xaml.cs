namespace MetaRov;

public partial class Carry : ContentPage
{
	public Carry()
	{
		InitializeComponent();
	}
    private async void videoBT_Clicked(object sender, EventArgs e)
    {
        string youtubeUrl = "https://www.youtube.com/watch?v=yr7LuzwiZFo";
        await Launcher.OpenAsync(youtubeUrl);
    }
}