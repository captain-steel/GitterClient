namespace GitterClient.Api
{
    using System;
    using System.Collections.Generic;
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
        /// The <see cref="IObservable" />.
        /// </returns>
        [Get("/rooms/{id}/chatMessages")]
        IObservable<IReadOnlyList<Message>> GetMessages([AliasAs("id")] string roomId, [Header("Authorization")] string accessToken);

        /// <summary>
        /// The get room users.
        /// </summary>
        /// <param name="roomId">The room id.</param>
        /// <param name="accessToken">The access token.</param>
        /// <returns>
        /// The <see cref="IObservable" />.
        /// </returns>
        [Get("/rooms/{id}/users")]
        IObservable<IReadOnlyList<User>> GetRoomUsers([AliasAs("id")] string roomId, [Header("Authorization")] string accessToken);

        /// <summary>
        /// The send message.
        /// </summary>
        /// <param name="roomId">The room id.</param>
        /// <param name="text">The text message.</param>
        /// <param name="accessToken">The access token.</param>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        [Post("rooms/{id}/chatMessages")]
        Task<Message> SendMessage([AliasAs("id")] string roomId, [Body] string text, [Header("Authorization")] string accessToken);

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
        [Put("rooms/{roomId}/chatMessages/{chatMessageId}")]
        Task<Message> UpdateMessage([AliasAs("roomId")] string roomId, [AliasAs("chatMessageId")] string chatMessageId, [Body] string text, [Header("Authorization")] string accessToken);
    }
}
