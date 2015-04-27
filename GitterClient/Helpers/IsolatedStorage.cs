namespace GitterClient.Helpers
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Windows.Storage;

    /// <summary>
    /// The isolated storage.
    /// </summary>
    public class IsolatedStorage
    {
        /// <summary>
        /// The save token.
        /// </summary>
        /// <param name="token">
        /// The token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public static async Task SaveToken(string token)
        {
            byte[] fileBytes = System.Text.Encoding.UTF8.GetBytes(token);

            // Get the local folder.
            StorageFolder local = ApplicationData.Current.LocalFolder;

            // Create a new file named DataFile.txt.
            var file = await local.CreateFileAsync("Token.txt", CreationCollisionOption.ReplaceExisting);

            // Write the data from the textbox.
            using (var stream = await file.OpenStreamForWriteAsync())
            {
                stream.Write(fileBytes, 0, fileBytes.Length);
            }
        }

        /// <summary>
        /// The get token.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public static async Task<string> GetToken()
        {
            var token = string.Empty;

            // Get the local folder.
            StorageFolder local = ApplicationData.Current.LocalFolder;

            if (local != null)
            {
                try
                {
                    // Get the file.
                    var file = await local.OpenStreamForReadAsync("Token.txt");

                    // Read the data.
                    using (var streamReader = new StreamReader(file))
                    {
                        token = streamReader.ReadToEnd();
                    }
                }
                catch (FileNotFoundException)
                {
                    token = string.Empty;
                }
            }

            return token;
        }
    }
}
