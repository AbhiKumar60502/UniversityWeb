using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Shm.Logging.ApplicationInsights
{
    /// <summary>
    /// ApplicationLogger is generically typed to expect T as either ILogger or ILogger&lt;K&gt;, where the ILogger is given
    /// a type to use as a logging category.
    /// See https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-5.0#log-category
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IApplicationLogger<T> : IApplicationLogger where T : class, ILogger
    {
    }

    public interface IApplicationLogger
    {
        IScopedApplicationLogger GetScopedApplicationLogger(IDictionary<string, string> properties);
        void Log(string eventName, string message, object properties, LogLevel level = LogLevel.Information, Exception exception = null);
        void Log(string eventName, string message, ICustomLoggable properties, LogLevel level = LogLevel.Information, Exception exception = null);
        void Log(string eventName, string message, IDictionary<string, string> properties = null, LogLevel level = LogLevel.Information, Exception exception = null);
        void TrackEvent(string eventName, object properties, double value = 1);
        void TrackEvent(string eventName, ICustomLoggable properties, double value = 1);
        void TrackEvent(string eventName, IDictionary<string, string> properties = null, double value = 1);
        void TrackEvent(string eventName, IDictionary<string, double> metrics, object properties);
        void TrackEvent(string eventName, IDictionary<string, double> metrics, ICustomLoggable properties);
        void TrackEvent(string eventName, IDictionary<string, double> metrics, IDictionary<string, string> properties = null);
    }
}