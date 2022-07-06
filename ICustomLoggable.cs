using System.Collections.Generic;

namespace Shm.Logging
{
    /// <summary>
    /// An interface to mark classes that require a custom method for creating a dictionary of properties to log.
    /// </summary>
    public interface ICustomLoggable
    {
        /// <summary>
        /// Returns a dictionary that represents the loggable properties of this instance
        /// </summary>
        IDictionary<string, string> ToLogProperties();
    }
}
