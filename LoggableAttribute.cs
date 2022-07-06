using System;

namespace Shm.Logging
{
    /// <summary>
    /// An attribute to mark properties that can be logged as a dictionary of property names and values.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class LoggableAttribute : Attribute
    {
    }
}
