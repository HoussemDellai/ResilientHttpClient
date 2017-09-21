
using Xamarin.Forms;

namespace ResilientHttpClientPcl
{
    public partial class App : Application
    {
        public App(IHttpClient httpClient)
        {
            InitializeComponent();

            MainPage = new ProductsPage()
            {
                BindingContext = new ProductsViewModel(httpClient),
            };
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
