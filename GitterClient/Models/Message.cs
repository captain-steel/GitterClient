namespace Models
{
    using System.Collections.Generic;

    /// <summary>
    /// The message.
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Gets or sets the edited at.
        /// </summary>
        public object EditedAt { get; set; }

        /// <summary>
        /// Gets or sets the from user.
        /// </summary>
        public User FromUser { get; set; }

        /// <summary>
        /// Gets or sets the html.
        /// </summary>
        public string Html { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the issues.
        /// </summary>
        public List<object> Issues { get; set; }

        /// <summary>
        /// Gets or sets the mentions.
        /// </summary>
        public List<object> Mentions { get; set; }

        /// <summary>
        /// Gets or sets the read by.
        /// </summary>
        public int ReadBy { get; set; }

        /// <summary>
        /// Gets or sets the sent.
        /// </summary>
        public string Sent { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether unread.
        /// </summary>
        public bool Unread { get; set; }

        /// <summary>
        /// Gets or sets the urls.
        /// </summary>
        public List<object> Urls { get; set; }

        /// <summary>
        /// Gets or sets the v.
        /// </summary>
        public int V { get; set; }
    }
}