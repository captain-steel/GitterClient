namespace Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// The send message.
    /// </summary>
    public class SendMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SendMessage"/> class.
        /// </summary>
        /// <param name="text">
        /// The text.
        /// </param>
        public SendMessage(string text)
        {
            Text = text;
        }

        /// <summary>
        /// Gets the text.
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; private set; }
    }
}