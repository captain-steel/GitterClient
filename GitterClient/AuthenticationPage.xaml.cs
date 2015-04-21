namespace GitterClient
{
    using System;
    using System.Diagnostics;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Windows.ApplicationModel.Activation;
    using Windows.Data.Json;
    using Windows.Security.Authentication.Web;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Navigation;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AuthenticationPage : IWebAuthenticationContinuable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationPage"/> class.
        /// </summary>
        public AuthenticationPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        /// <summary>
        /// The connect click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void ConnectClick(object sender, RoutedEventArgs e)
        {
            const string gitterUrl = "https://gitter.im/login/oauth/authorize?response_type=code&redirect_uri=" + "http://localhost" + "&client_id=" + "cbcfeee1efe6e37d386e6ec58e4df68339e567e7";

            var startUri = new Uri(gitterUrl);
            var endUri = new Uri("http://localhost");

            WebAuthenticationBroker.AuthenticateAndContinue(startUri, endUri, null, WebAuthenticationOptions.None);
        }

        /// <summary>
        /// The continue web authentication.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        public async void ContinueWebAuthentication(WebAuthenticationBrokerContinuationEventArgs args)
        {
            WebAuthenticationResult result = args.WebAuthenticationResult;

            if (result.ResponseStatus == WebAuthenticationStatus.Success)
            {
                Debug.WriteLine(result.ResponseData);
                await GetFacebookUserNameAsync(result.ResponseData);
            }
            else if (result.ResponseStatus == WebAuthenticationStatus.ErrorHttp)
            {
                Debug.WriteLine("HTTP Error returned by AuthenticateAsync() : " + result.ResponseErrorDetail);
            }
            else
            {
                Debug.WriteLine("Error returned by AuthenticateAsync() : " + result.ResponseStatus);
            }
        }

        /// <summary>
        /// This function extracts access_token from the response returned from web authentication broker
        /// and uses that token to get user information using facebook graph api. 
        /// </summary>
        /// <param name="webAuthResultResponseData">
        /// responseData returned from AuthenticateAsync result.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        private async Task GetFacebookUserNameAsync(string webAuthResultResponseData)
        {
            // Get Access Token first
            string responseData = webAuthResultResponseData.Substring(webAuthResultResponseData.IndexOf("access_token"));
            var keyValPairs = responseData.Split('&');
            string accessToken = null;

            foreach (string keyValue in keyValPairs)
            {
                string[] splits = keyValue.Split('=');
                switch (splits[0])
                {
                    case "access_token":
                        accessToken = splits[1];
                        break;
                    case "expires_in":
                        break;
                }
            }

            Debug.WriteLine("access_token = " + accessToken);

            // Request User info.
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(new Uri("https://graph.facebook.com/me?access_token=" + accessToken));
            var value = JsonValue.Parse(response).GetObject();
            var facebookUserName = value.GetNamedString("name");
        }
    }
}
