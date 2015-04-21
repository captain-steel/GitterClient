namespace GitterClient.Services
{
    using System;
    using Models;

    /// <summary>
    /// The GitterStreamingApi interface.
    /// </summary>
    public interface IGitterStreamingApi
    {
        /// <summary>
        /// The observe messages.
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
        IObservable<Message> ObserveMessages(string roomId, string accessToken);
    }
}
