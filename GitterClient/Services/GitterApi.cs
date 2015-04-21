namespace GitterClient.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Reactive.Linq;
    using Akavache;
    using Fusillade;
    using Refit;

    /// <summary>
    /// The gitter api.
    /// </summary>
    public class GitterApi
    {
        /// <summary>
        /// The api base address.
        /// </summary>
        public static readonly string ApiBaseAddress = "https://api.gitter.im/v1";

        /// <summary>
        /// The user initiated.
        /// </summary>
        private static readonly Lazy<IGitterApi> LazyUserInitiated;

        /// <summary>
        /// Initializes static members of the <see cref="GitterApi"/> class.
        /// </summary>
        static GitterApi()
        {
            LazyUserInitiated = new Lazy<IGitterApi>(() =>
            {
                var client = new HttpClient(NetCache.UserInitiated)
                {
                    BaseAddress = new Uri(ApiBaseAddress)
                };

                return RestService.For<IGitterApi>(client);
            });
        }

        /// <summary>
        /// Gets the user initiated.
        /// </summary>
        public static IGitterApi UserInitiated
        {
            get { return LazyUserInitiated.Value; }
        }

        /// <summary>
        /// Gets the formatted access token ready o be passed directly into the REST API.
        /// </summary>
        /// <exception cref="KeyNotFoundException">
        /// The access token isn't stored.
        /// </exception>
        /// <returns>
        /// The <see cref="IObservable"/>.
        /// </returns>
        public static IObservable<string> GetAccessToken()
        {
            return Observable.Defer(() => BlobCache.Secure.GetLoginAsync("Gitter")).Select(x => "Bearer " + x.Password);
        }
    }
}
