namespace GitterClient.Services
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Reactive.Linq;
    using System.Reactive.Threading.Tasks;
    using System.Text;
    using System.Threading.Tasks;
    using Models;
    using ModernHttpClient;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// The gitter streaming api.
    /// </summary>
    public class GitterStreamingApi : IGitterStreamingApi
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
        public IObservable<Message> ObserveMessages(string roomId, string accessToken)
        {
            string url = string.Format("https://stream.gitter.im/v1/rooms/{0}/chatMessages", roomId);

            return Observable.Using(
            () =>
            {
                var client = new HttpClient(new NativeMessageHandler());
                client.DefaultRequestHeaders.Add("Authorization", accessToken);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                return client;
            },
                client => client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead).ToObservable()
                                .SelectMany(x => x.Content.ReadAsStreamAsync())
                                .Select(x => Observable.FromAsync(() => ReadLine(x)).Repeat())
                                .Concat()
                                .Where(x => !string.IsNullOrWhiteSpace(x))
                                .Select(x => JObject.Parse(x).ToObject<Message>()));
        }

        /// <summary>
        /// The read line.
        /// </summary>
        /// <param name="stream">
        /// The stream.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        private async Task<string> ReadLine(Stream stream)
        {
            using (var reader = new StreamReader(stream, Encoding.UTF8, false, 1024, true))
            {
                string line = await reader.ReadLineAsync();

                return line;
            }
        }
    }
}
