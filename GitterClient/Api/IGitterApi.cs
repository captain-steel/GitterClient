namespace GitterClient.Api
{
    using System.Collections.ObjectModel;
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
        [Get("/rooms/{id}/chatMessages")]
        Task<ObservableCollection<Message>> GetMessages([AliasAs("id")] string roomId, [Header("Authorization")] string accessToken);

        /// <summary>
        /// The get room users.
        /// </summary>
        /// <param name="roomId">The room id.</param>
        /// <param name="accessToken">The access token.</param>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        [Get("/rooms/{id}/users")]
        Task<ObservableCollection<User>> GetRoomUsers([AliasAs("id")] string roomId, [Header("Authorization")] string accessToken);

        /*[Post("rooms/{id}/chatMessages")]
        Task<Message> SendMessage([AliasAs("id")] string roomId, [Body] string text, [Header("Authorization")] string accessToken);*/

        /*[Put("rooms/{roomId}/chatMessages/{chatMessageId}")]
        Task<Message> UpdateMessage([AliasAs("roomId")] string roomId, [AliasAs("chatMessageId")] string chatMessageId, [Body] string text, [Header("Authorization")] string accessToken);*/
    }
}
