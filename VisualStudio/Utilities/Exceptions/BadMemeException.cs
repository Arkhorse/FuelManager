// https://github.com/Willster419/RelhaxModpack/blob/master/RelhaxModpack/RelhaxModpack/Common/BadMemeException.cs

namespace FuelManager
{
    /// <summary>
    /// An exception used mostly for mistakes that could happen during development, 'sanity check' type verification. And also for bad memes.
    /// </summary>
    [Serializable]
    public class BadMemeException : Exception
    {
        /// <summary>
        /// Throw a bad meme exception.
        /// </summary>
        /// <param name="message">The message to tell the developer why his meme is bad.</param>
        public BadMemeException(string message) : base(message)
        {

        }
    }
}
