namespace GitterClient
{
    using System;

    /// <summary>
    /// The suspension manager exception.
    /// </summary>
    public class SuspensionManagerException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SuspensionManagerException"/> class.
        /// </summary>
        public SuspensionManagerException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SuspensionManagerException"/> class.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        public SuspensionManagerException(Exception e) : base("SuspensionManager failed", e)
        {
        }
    }
}