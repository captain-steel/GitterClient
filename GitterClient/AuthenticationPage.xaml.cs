namespace GitterClient
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    using GitterClient.Common;
    using GitterClient.Helpers;

    using Windows.ApplicationModel.Activation;
    using Windows.Data.Json;
    using Windows.Security.Authentication.Web;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AuthenticationPage : IWebAuthenticationContinuable
    {
        /// <summary>
        /// The dispatcher timer.
        /// </summary>
        private readonly DispatcherTimer _dispatcherTimer = new DispatcherTimer();

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationPage"/> class.
        /// </summary>
        public AuthenticationPage()
        {
            InitializeComponent();

            _dispatcherTimer.Interval = new TimeSpan(0, 0, 0);
            _dispatcherTimer.Tick += DispatcherTick;
            _dispatcherTimer.Start();
        }

        /// <summary>
        /// The dispatcher tick.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args</param>
        /// <remarks>http://stackoverflow.com/questions/23267918</remarks>
        private async void DispatcherTick(object sender, object e)
        {
            _dispatcherTimer.Stop();
            
            var token = await IsolatedStorage.GetToken();
            if (!string.IsNullOrEmpty(token))
            {
                ((Frame)Window.Current.Content).Navigate(typeof(RoomsPage));

                return;
            }

            var gitterUrl = string.Format("{0}{1}?response_type=code&redirect_uri={2}&client_id={3}", Constants.GitterBaseAddress, Constants.AuthEndPoint, Constants.RedirectUrl, Constants.ClientKey);

            var startUri = new Uri(gitterUrl);
            var endUri = new Uri(Constants.RedirectUrl);

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
                    new KeyValuePair<string, string>("grant_type", "authorization_code")
                });

                var result = await httpClient.PostAsync(Constants.TokenEndpoint, content);
                string resultContent = result.Content.ReadAsStringAsync().Result;
                JsonObject value = JsonValue.Parse(resultContent).GetObject();
                string accessToken = value.GetNamedString("access_token");

                Debug.WriteLine("Access Token = " + accessToken);
                
                await IsolatedStorage.SaveToken(string.Format("Bearer {0}", accessToken));
            }

            Frame.Navigate(typeof(RoomsPage));
        }
    }
}
