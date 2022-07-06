using System;

namespace Shm.Logging
{
    /// <summary>
    /// An attribute to mark classes that can be logged and are expected to mark loggable properties with the Loggable attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class LoggableEntityAttribute : Attribute
    {
    }
}
