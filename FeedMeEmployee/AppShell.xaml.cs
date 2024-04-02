using FeedMeEmployee.Views;

namespace FeedMeEmployee
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(Historique), typeof(Historique));
        }
    }
}
