namespace GitterClient
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Api;
    using Common;
    using Refit;
    using Windows.ApplicationModel.Activation;
    using Windows.Data.Json;
    using Windows.Security.Authentication.Web;
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
                await GetAccessToken(result.ResponseData);
            }
            else if (result.ResponseStatus == WebAuthenticationStatus.ErrorHttp)
            {
                Debug.WriteLine("HTTP Error : " + result.ResponseErrorDetail);
            }
            else
            {
                Debug.WriteLine("Error : " + result.ResponseStatus);
            }
        }

        /// <summary>
        /// The get access token.
        /// </summary>
        /// <param name="authorizationCode">
        /// The web auth result response data.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        private async Task GetAccessToken(string authorizationCode)
        {
            var authCode = authorizationCode.Split('=')[1];

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(Constants.GitterBaseAddress);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = new FormUrlEncodedContent(new[] 
                {
                    new KeyValuePair<string, string>("client_id", Constants.ClientKey),
                    new KeyValuePair<string, string>("client_secret", Constants.OauthSecret),
                    new KeyValuePair<string, string>("code", authCode),
                    new KeyValuePair<string, string>("redirect_uri", Constants.RedirectUrl),
                    new KeyValuePair<string, string>("grant_type", "authorization_code"),
                });

                var result = await httpClient.PostAsync(Constants.TokenEndpoint, content);
                string resultContent = result.Content.ReadAsStringAsync().Result;
                JsonObject value = JsonValue.Parse(resultContent).GetObject();
                string accessToken = value.GetNamedString("access_token");

                Debug.WriteLine("Access Token = " + accessToken);
                
                // App.Token = string.Format("Bearer {0}", accessToken);
            }

            Frame.Navigate(typeof(RoomsPage));
        }
    }
}
