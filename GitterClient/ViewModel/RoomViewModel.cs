namespace GitterClient.ViewModel
{
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Messaging;
    using GalaSoft.MvvmLight.Views;

    /// <summary>
    /// The room view model.
    /// </summary>
    public class RoomViewModel : ViewModelBase
    {
        #region Private fields
        
        /// <summary>
        /// The navigation service.
        /// </summary>
        private INavigationService _navigationService;

        #endregion

        #region Properties

        /// <summary>
        /// The _room.
        /// </summary>
        private Room _room;

        /// <summary>
        /// Gets or sets the room.
        /// </summary>
        public Room Room
        {
            get { return _room; }
            set { Set(() => Room, ref _room, value); }
        }

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="RoomViewModel"/> class.
        /// </summary>
        /// <param name="navigationService">
        /// The navigation service.
        /// </param>
        public RoomViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            Messenger.Default.Register<Room>(
                    this,
                    room =>
                    {
                        Room = room;
                    });
        }

        #endregion
    }
}
