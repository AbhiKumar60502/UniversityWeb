using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Shm.Logging.ApplicationInsights
{
    /// <summary>
    /// ScopedApplicationLogger supports the logical grouping of a set of logging operations. The given constructor properties are
    /// mapped to customDimensions for every logging call with the scope.
    /// </summary>
    public class ScopedApplicationLogger<T> : IScopedApplicationLogger where T : class, ILogger
    {
        private readonly IApplicationLogger<T> _applicationLogger;
        private readonly IDictionary<string, string> _properties;

        public ScopedApplicationLogger(IApplicationLogger<T> applicationLogger, IDictionary<string, string> properties)
        {
            _applicationLogger = applicationLogger ?? throw new ArgumentNullException(nameof(applicationLogger));

            if (properties == null)
            {
                throw new ArgumentNullException(nameof(properties));
            }

            _properties = properties.ToDictionary(p => p.Key, p => p.Value);
        }

        public void Dispose()
        {
            _properties.Clear();
        }

        /// <summary>
        /// Get a IScopedApplicationLogger to support the logical grouping of a set of logging operations. The given properties are
        /// mapped to customDimensions for every logging call with the scope.
        /// </summary>
        /// <param name="properties"></param>
        public IScopedApplicationLogger GetScopedApplicationLogger(IDictionary<string, string> properties)
        {
            return new ScopedApplicationLogger<T>(_applicationLogger, GetDimensions(properties));
        }

        /// <summary>
        /// Write an entry to Application Insights trace table, with a loggable properties instance and optional log level. If exception is provided, an entry is also written
        /// to the Exceptions table.
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="message"></param>
        /// <param name="properties"></param>
        /// <param name="level"></param>
        /// <param name="exception"></param>
        public void Log(string eventName, string message, object properties, LogLevel level = LogLevel.Information, Exception exception = null)
        {
            var propertiesDictionary = LoggingHelper.GetPropertiesWithAttribute(properties);
            var dimensions = GetDimensions(propertiesDictionary);
            _applicationLogger.Log(eventName, message, dimensions, level, exception);
        }

        /// <summary>
        /// Write an entry to Application Insights trace table, with a custom loggable instance and optional log level. If exception is provided, an entry is also written
        /// to the Exceptions table.
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="message"></param>
        /// <param name="properties"></param>
        /// <param name="level"></param>
        /// <param name="exception"></param>
        public void Log(string eventName, string message, ICustomLoggable properties, LogLevel level = LogLevel.Information, Exception exception = null)
        {
            var dimensions = GetDimensions(properties.ToLogProperties());
            _applicationLogger.Log(eventName, message, dimensions, level, exception);
        }

        /// <summary>
        /// Write an entry to Application Insights trace table, with optional properties and log level. If exception is also provided, an entry is instead written
        /// to the Exceptions table.
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="message"></param>
        /// <param name="properties"></param>
        /// <param name="level"></param>
        /// <param name="exception"></param>
        public void Log(string eventName, string message, IDictionary<string, string> properties = null, LogLevel level = LogLevel.Information, Exception exception = null)
        {
            var dimensions = GetDimensions(properties);
            _applicationLogger.Log(eventName, message, dimensions, level, exception);
        }

        /// <summary>
        /// Write an entry to the Application Insights customEvents table, with a loggable properties instance.
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="properties"></param>
        /// <param name="value"></param>
        public void TrackEvent(string eventName, object properties, double value = 1)
        {
            var propertiesDictionary = LoggingHelper.GetPropertiesWithAttribute(properties);
            var dimensions = GetDimensions(propertiesDictionary);
            _applicationLogger.TrackEvent(eventName, dimensions, value);
        }

        /// <summary>
        /// Write an entry to the Application Insights customEvents table, with a custom loggable properties instance.
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="properties"></param>
        /// <param name="value"></param>
        public void TrackEvent(string eventName, ICustomLoggable properties, double value = 1)
        {
            var dimensions = GetDimensions(properties.ToLogProperties());
            _applicationLogger.TrackEvent(eventName, dimensions, value);
        }

        /// <summary>
        /// Write an entry to the Application Insights customEvents table, with optional properties.
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="properties"></param>
        /// <param name="value"></param>
        public void TrackEvent(string eventName, IDictionary<string, string> properties = null, double value = 1)
        {
            var dimensions = GetDimensions(properties);
            _applicationLogger.TrackEvent(eventName, dimensions, value);
        }

        /// <summary>
        /// Write an entry to the Application Insights customEvents table, with a loggable properties instance.
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="metrics"></param>
        /// <param name="properties"></param>
        public void TrackEvent(string eventName, IDictionary<string, double> metrics, object properties)
        {
            var propertiesDictionary = LoggingHelper.GetPropertiesWithAttribute(properties);
            var dimensions = GetDimensions(propertiesDictionary);
            _applicationLogger.TrackEvent(eventName, metrics, dimensions);
        }

        /// <summary>
        /// Write an entry to the Application Insights customEvents table, with optional properties.
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="metrics"></param>
        /// <param name="properties"></param>
        public void TrackEvent(string eventName, IDictionary<string, double> metrics, ICustomLoggable properties)
        {
            var dimensions = GetDimensions(properties.ToLogProperties());
            _applicationLogger.TrackEvent(eventName, metrics, dimensions);
        }

        /// <summary>
        /// Write an entry to the Application Insights customEvents table, with optional properties.
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="metrics"></param>
        /// <param name="properties"></param>
        public void TrackEvent(string eventName, IDictionary<string, double> metrics, IDictionary<string, string> properties = null)
        {
            var dimensions = GetDimensions(properties);
            _applicationLogger.TrackEvent(eventName, metrics, dimensions);
        }

        private Dictionary<string, string> GetDimensions(IDictionary<string, string> properties)
        {
            var dimensions = _properties.ToDictionary(p => p.Key, p => p.Value);

            if (properties != null)
            {
                foreach (var property in properties)
                {
                    dimensions[property.Key] = property.Value;
                }
            }

            return dimensions;
        }
    }
}
