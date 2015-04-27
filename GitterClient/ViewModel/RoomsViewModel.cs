namespace GitterClient.ViewModel
{
    using System.Collections.ObjectModel;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using Api;
    using Common;
    using Refit;

    /// <summary>
    /// The rooms view model.
    /// </summary>
    public class RoomsViewModel : ViewModelBase
    {
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
        public RoomsViewModel()
        {
            GetRooms();
        }

        #endregion

        #region RelayCommands

        /// <summary>
        /// The _enter room command.
        /// </summary>
        private RelayCommand _enterRoomCommand;

        /// <summary>
        /// Gets the enter room command.
        /// </summary>
        public RelayCommand EnterRoomCommand
        {
            get { return _enterRoomCommand ?? (_enterRoomCommand = new RelayCommand(EnterRoomCommandExecute)); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The get rooms.
        /// </summary>
        private async void GetRooms()
        {
            var client = RestService.For<IGitterApi>(Constants.GitterApi);

            RoomsList = await client.GetRooms(App.Token);
        }

        /// <summary>
        /// The enter room command execute.
        /// </summary>
        private void EnterRoomCommandExecute()
        {

        }

        #endregion
    }
}
