using ResilientHttpClientPcl.Std.ResilienceHttp;

namespace ResilientHttpClientPcl.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            var factory = new ResilientHttpClientFactory();
            var httpClient = factory.CreateResilientHttpClient();

            LoadApplication(new ResilientHttpClientPcl.App(httpClient));
        }
    }
}
