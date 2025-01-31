namespace MetaRov
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var np = new NavigationPage(new MainPage());
            MainPage = np;
        }
    }
}
