namespace GitterClient.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Messaging;
    using GalaSoft.MvvmLight.Views;

    using GitterClient.Api;
    using GitterClient.Common;
    using GitterClient.Helpers;

    using Newtonsoft.Json;

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
        /// The message.
        /// </summary>
        private string _message;

        /// <summary>
        /// The _room.
        /// </summary>
        private Room _room;

        /// <summary>
        /// The messages list.
        /// </summary>
        private ObservableCollection<Message> _messagesList;

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string Message
        {
            get
            {
                return _message;
            }
            
            set
            {
                _message = value;
                this.RaisePropertyChanged("Message");
            }
        }

        /// <summary>
        /// Gets or sets the room.
        /// </summary>
        public Room Room
        {
            get { return _room; }
            set { Set(() => Room, ref _room, value); }
        }

        /// <summary>
        /// Gets or sets the messages list.
        /// </summary>
        public ObservableCollection<Message> MessagesList
        {
            get { return _messagesList; }
            set { Set(() => MessagesList, ref _messagesList, value); }
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
            if (IsInDesignMode)
            {
                MessagesList = new ObservableCollection<Message>
                                   {
                                       new Message
                                           {
                                               fromUser = new User { displayName = "Captain"},
                                               text = "hello world"
                                           },

                                        new Message
                                           {
                                               fromUser = new User { displayName = "Steel"},
                                               text = "hello everybody"
                                           },
                                   };
                return;
            }

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

        #region RelayCommand

        /// <summary>
        /// The send message command.
        /// </summary>
        private RelayCommand _sendMessageCommand;

        /// <summary>
        /// Gets the send message command.
        /// </summary>
        public RelayCommand SendMessageCommand
        {
            get { return _sendMessageCommand ?? (_sendMessageCommand = new RelayCommand(SendMessageCommandExecute)); }
        }

        /// <summary>
        /// The send message command execute.
        /// </summary>
        private async void SendMessageCommandExecute()
        {
            var client = RestService.For<IGitterApi>(Constants.GitterApi);

            await client.SendMessage(Room.id, new SendMessage(Message), await IsolatedStorage.GetToken());
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

            MessagesList = await client.GetMessages(room.id, await IsolatedStorage.GetToken());
        }

        #endregion
    }
}
