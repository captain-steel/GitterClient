namespace GitterClient.ViewModel
{
    using System.Collections.Generic;
    using GalaSoft.MvvmLight;
    using ReactiveUI;

    /// <summary>
    /// The rooms view model.
    /// </summary>
    public class RoomsViewModel : ViewModelBase
    {
        /// <summary>
        /// The selected room.
        /// </summary>
        private RoomViewModel _selectedRoom;

        /// <summary>
        /// Gets or sets the selected room.
        /// </summary>
        public RoomViewModel SelectedRoom
        {
            get { return _selectedRoom; }
            set { Set(() => SelectedRoom, ref _selectedRoom, value); }
        }

        /// <summary>
        /// Gets the rooms.
        /// </summary>
        public IReactiveList<RoomViewModel> Rooms { get; private set; }

        /// <summary>
        /// Gets the load rooms.
        /// </summary>
        public ReactiveCommand<IEnumerable<RoomViewModel>> LoadRooms { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RoomsViewModel"/> class.
        /// </summary>
        public RoomsViewModel()
        {
            Rooms = new ReactiveList<RoomViewModel>();
        }
    }
}
