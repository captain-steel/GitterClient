using Windows.UI.Xaml.Navigation;

namespace GitterClient
{
    using System;
    using System.Diagnostics;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Windows.ApplicationModel.Activation;
    using Windows.Data.Json;
    using Windows.Phone.UI.Input;
    using Windows.Security.Authentication.Web;
    using Windows.UI.Xaml;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage
    {
        /// <summary>
        /// The redirect url.
        /// </summary>
        private const string RedirectUrl = "http://www.wassimazirar.com";

        /// <summary>
        /// The oauth key.
        /// </summary>
        private const string OauthKey = "b9b36352177ed6b203e75958eb627672d12a6c10";

        /// <summary>
        /// The oauth secret.
        /// </summary>
        private const string OauthSecret = "75f3c739cd011d1b2a218fe04f615adb62a19f49";

        /// <summary>
        /// The auth endpoint.
        /// </summary>
        private readonly Uri _authEndpoint = new Uri("https://gitter.im/login/oauth/authorize");

        /// <summary>
        /// Gets or sets the current.
        /// </summary>
        public static MainPage Current { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPage"/> class.
        /// </summary>
        public MainPage()
        {
            InitializeComponent();

            Current = this;

            HardwareButtons.BackPressed += HardwareButtonsBackPressed;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SuspensionManager.RegisterFrame(ScenarioFrame, "scenarioFrame");
            if (ScenarioFrame.Content == null)
            {
                ScenarioFrame.Navigate(typeof(AuthenticationPage));
            }
        } 

        /// <summary>
        /// The hardware buttons back pressed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="eventArgs">
        /// The event args.
        /// </param>
        private void HardwareButtonsBackPressed(object sender, BackPressedEventArgs eventArgs)
        {
            if (ScenarioFrame.CanGoBack)
            {
                ScenarioFrame.GoBack();

                // Indicate the back button press is handled so the app does not exit
                eventArgs.Handled = true;
            }
        }
    }
}