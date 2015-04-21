namespace GitterClient.Services
{
    using System;
    using System.Collections.Generic;
    using System.Reactive;
    using System.Threading.Tasks;
    using Models;
    using Refit;

    /// <summary>
    /// The GitterApi interface.
    /// </summary>
    [Headers("Accept: application/json")]
    public interface IGitterApi
    {
        /// <summary>
        /// The get messages.
        /// </summary>
        /// <param name="roomId">
        /// The room id.
        /// </param>
        /// <param name="accessToken">
        /// The access token.
        /// </param>
        /// <returns>
        /// The <see cref="IObservable"/>.
        /// </returns>
        [Get("/rooms/{id}/chatMessages")]
        IObservable<IReadOnlyList<Message>> GetMessages([AliasAs("id")] string roomId, [Header("Authorization")] string accessToken);

        /// <summary>
        /// The get room users.
        /// </summary>
        /// <param name="roomId">
        /// The room id.
        /// </param>
        /// <param name="accessToken">
        /// The access token.
        /// </param>
        /// <returns>
        /// The <see cref="IObservable"/>.
        /// </returns>
        [Get("/rooms/{id}/users")]
        IObservable<IReadOnlyList<User>> GetRoomUsers([AliasAs("id")] string roomId, [Header("Authorization")] string accessToken);

        /// <summary>
        /// The get rooms.
        /// </summary>
        /// <param name="accessToken">
        /// The access token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Get("/rooms")]
        Task<IReadOnlyList<Room>> GetRooms([Header("Authorization")] string accessToken);

        /// <summary>
        /// The send message.
        /// </summary>
        /// <param name="roomId">
        /// The room id.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="accessToken">
        /// The access token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Post("/rooms/{id}/chatMessages")]
        Task<Unit> SendMessage([AliasAs("id")] string roomId, [Body] SendMessage message, [Header("Authorization")] string accessToken);
    }
}
