namespace GitterClient.ViewModel
{
    using System.Reactive.Linq;

    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Messaging;
    using GalaSoft.MvvmLight.Views;

    using GitterClient.Api;
    using GitterClient.Common;
    using GitterClient.Helpers;

    using Refit;

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
                        GetMessages(Room);
                    });
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the messages.
        /// </summary>
        /// <param name="room">The room.</param>
        private async void GetMessages(Room room)
        {
            var client = RestService.For<IGitterApi>(Constants.GitterApi);

            var messages = await client.GetMessages(room.id, await IsolatedStorage.GetToken());
        }

        #endregion
    }
}
