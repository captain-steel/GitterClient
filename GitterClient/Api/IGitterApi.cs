namespace GitterClient.Api
{
    using System.Collections.ObjectModel;
    using System.Reactive;
    using System.Threading.Tasks;

    using Refit;

    /// <summary>
    /// The GitterApi interface.
    /// </summary>
    [Headers("Accept: application/json")]
    public interface IGitterApi
    {
        /// <summary>
        /// The get rooms.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        [Get("/rooms")]
        Task<ObservableCollection<Room>> GetRooms([Header("Authorization")] string accessToken);

        /// <summary>
        /// The get messages.
        /// </summary>
        /// <param name="roomId">The room id.</param>
        /// <param name="accessToken">The access token.</param>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        [Get("/rooms/{roomId}/chatMessages")]
        Task<ObservableCollection<Message>> GetMessages(string roomId, [Header("Authorization")] string accessToken, int limit = 20);

        /// <summary>
        /// The get room users.
        /// </summary>
        /// <param name="roomId">The room id.</param>
        /// <param name="accessToken">The access token.</param>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        [Get("/rooms/{roomId}/users")]
        Task<ObservableCollection<User>> GetRoomUsers(string roomId, [Header("Authorization")] string accessToken);

        /// <summary>
        /// The send message.
        /// </summary>
        /// <param name="roomId">The room id.</param>
        /// <param name="message">The message.</param>
        /// <param name="accessToken">The access token.</param>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        [Post("/rooms/{roomId}/chatMessages")]
        Task<Unit> SendMessage(string roomId, [Body] SendMessage message, [Header("Authorization")] string accessToken);

        /// <summary>
        /// The update message.
        /// </summary>
        /// <param name="roomId">The room id.</param>
        /// <param name="chatMessageId">The chat message id.</param>
        /// <param name="text">The text.</param>
        /// <param name="accessToken">The access token.</param>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        [Put("/rooms/{roomId}/chatMessages/{chatMessageId}")]
        Task<Unit> UpdateMessage(string roomId, string chatMessageId, [Body] string text, [Header("Authorization")] string accessToken);
    }
}
