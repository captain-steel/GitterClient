namespace Models
{
    /// <summary>
    /// The room.
    /// </summary>
    public class Room
    {
        /// <summary>
        /// Gets or sets the github type.
        /// </summary>
        public string GithubType { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the last access time.
        /// </summary>
        public string LastAccessTime { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether lurk.
        /// </summary>
        public bool Lurk { get; set; }

        /// <summary>
        /// Gets or sets the mentions.
        /// </summary>
        public int Mentions { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether one to one.
        /// </summary>
        public bool OneToOne { get; set; }

        /// <summary>
        /// Gets or sets the security.
        /// </summary>
        public string Security { get; set; }

        /// <summary>
        /// Gets or sets the topic.
        /// </summary>
        public string Topic { get; set; }

        /// <summary>
        /// Gets or sets the unread items.
        /// </summary>
        public int UnreadItems { get; set; }

        /// <summary>
        /// Gets or sets the uri.
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// Gets or sets the url.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Gets or sets the user count.
        /// </summary>
        public int? UserCount { get; set; }

        /// <summary>
        /// Gets or sets the v.
        /// </summary>
        public int? V { get; set; }
    }
}