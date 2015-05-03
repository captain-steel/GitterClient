namespace GitterClient.ViewModel
{
    using System.Collections.ObjectModel;

    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Messaging;
    using GalaSoft.MvvmLight.Views;

    using GitterClient.Api;
    using GitterClient.Common;
    using GitterClient.Helpers;

    using Refit;

    /// <summary>
    /// The rooms view model.
    /// </summary>
    public class RoomsViewModel : ViewModelBase
    {
        #region Private fields

        /// <summary>
        /// The navigation service.
        /// </summary>
        private INavigationService _navigationService;

        #endregion

        #region Properties

        /// <summary>
        /// The rooms list.
        /// </summary>
        private ObservableCollection<Room> _roomsList;

        /// <summary>
        /// Gets or sets the rooms list.
        /// </summary>
        public ObservableCollection<Room> RoomsList
        {
            get
            {
                return _roomsList;
            }
            
            set
            {
                _roomsList = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="RoomsViewModel"/> class.
        /// </summary>
        /// <param name="navigationService">
        /// The navigation service.
        /// </param>
        public RoomsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            GetRooms();
        }

        #endregion

        #region RelayCommands

        /// <summary>
        /// The _enter room command.
        /// </summary>
        private RelayCommand<Room> _enterRoomCommand;

        /// <summary>
        /// Gets the enter room command.
        /// </summary>
        public RelayCommand<Room> EnterRoomCommand
        {
            get { return _enterRoomCommand ?? (_enterRoomCommand = new RelayCommand<Room>(EnterRoomCommandExecute)); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The get rooms.
        /// </summary>
        private async void GetRooms()
        {
            var client = RestService.For<IGitterApi>(Constants.GitterApi);

            RoomsList = await client.GetRooms(await IsolatedStorage.GetToken());
        }

        /// <summary>
        /// The enter room command execute.
        /// </summary>
        /// <param name="room">
        /// The room.
        /// </param>
        private void EnterRoomCommandExecute(object room)
        {
            Messenger.Default.Send((Room)room);
            
            /*var navigationService = ServiceLocator.Current.GetInstance<INavigationService>();
            navigationService.NavigateTo("Room");*/

            _navigationService.NavigateTo("Room");
        }

        #endregion
    }
}
