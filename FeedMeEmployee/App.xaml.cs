using FeedMeEmployee.ViewModels;

namespace FeedMeEmployee
{
    public partial class App : Application
    {
        public App(IServiceProvider provider)
        {
            InitializeComponent();
            //MainPage = new MainPage();
            MainPage = new AppShell();
        }
    }
}
