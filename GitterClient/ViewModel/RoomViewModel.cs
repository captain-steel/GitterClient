namespace GitterClient.ViewModel
{
    using System;
    using Models;

    /// <summary>
    /// The room view model.
    /// </summary>
    public class RoomViewModel
    {
        /// <summary>
        /// Gets the room.
        /// </summary>
        public Room Room { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RoomViewModel"/> class.
        /// </summary>
        /// <param name="room">
        /// The room.
        /// </param>
        public RoomViewModel(Room room)
        {
            if (room == null)
            {
                throw new ArgumentNullException("room");
            }

            Room = room;
        }

        /// <summary>
        /// Gets the user avatar source.
        /// </summary>
        public Uri UserAvatarSource
        {
            get
            {
                if (Room.user != null && Room.user.avatarUrlSmall != null)
                {
                    return new Uri(Room.user.avatarUrlSmall);
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the id.
        /// </summary>
        public string Id
        {
            get { return Room.id; }
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name
        {
            get { return Room.name; }
        }

        /// <summary>
        /// Gets the topic.
        /// </summary>
        public string Topic
        {
            get { return Room.topic; }
        }
    }
}
