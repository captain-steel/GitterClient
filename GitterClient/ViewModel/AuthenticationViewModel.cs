using GitterClient.Helpers;

namespace GitterClient.ViewModel
{
    using System;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using Common;
    using Windows.Security.Authentication.Web;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// The authentication view model.
    /// </summary>
    public class AuthenticationViewModel : ViewModelBase
    {
        #region RelayCommands

        /// <summary>
        /// The connect command.
        /// </summary>
        private RelayCommand _connectCommand;
        
        /// <summary>
        /// Gets the connect command.
        /// </summary>
        public RelayCommand ConnectCommand
        {
            get { return _connectCommand ?? (_connectCommand = new RelayCommand(ConnectCommandExecute)); }
        }
        
        #endregion

        /// <summary>
        /// The connect command execute.
        /// </summary>
        private async void ConnectCommandExecute()
        {
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
    }
}
