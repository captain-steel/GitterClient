using System.Collections.ObjectModel;

namespace GitterClient.Api
{
    using System;
    using System.Collections.Generic;
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
        /// <param name="accessToken">
        /// The access token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Get("/rooms")]
        Task<ObservableCollection<Room>> GetRooms([Header("Authorization")] string accessToken);

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
    }
}
