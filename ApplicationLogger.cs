using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Logging;

namespace Shm.Logging.ApplicationInsights
{
    /// <summary>
    /// ApplicationLogger is generically typed to expect T as either ILogger or ILogger&lt;K&gt;, where the ILogger is given
    /// a type to use as a logging category.
    /// See https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-5.0#log-category
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApplicationLogger<T> : IApplicationLogger<T> where T : class, ILogger
    {
        private readonly TelemetryClient _telemetryClient;
        private readonly T _logger;

        /// <summary>
        /// See https://docs.microsoft.com/en-us/azure/azure-monitor/app/api-custom-events-metrics
        /// </summary>
        public ApplicationLogger(TelemetryClient telemetryClient, T logger)
        {
            _telemetryClient = telemetryClient ?? throw new ArgumentNullException(nameof(telemetryClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Get a IScopedApplicationLogger to support the logical grouping of a set of logging operations. The given properties are
        /// mapped to customDimensions for every logging call with the scope.
        /// </summary>
        /// <param name="properties"></param>
        public IScopedApplicationLogger GetScopedApplicationLogger(IDictionary<string, string> properties)
        {
            if (properties == null)
            {
                throw new ArgumentNullException(nameof(properties));
            }

            return new ScopedApplicationLogger<T>(this, properties);
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
            if (properties == null)
            {
                throw new ArgumentNullException(nameof(properties));
            }

            var propertiesDictionary = LoggingHelper.GetPropertiesWithAttribute(properties);
            Log(eventName, message, propertiesDictionary, level, exception);
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
            if (properties == null)
            {
                throw new ArgumentNullException(nameof(properties));
            }

            var propertiesDictionary = properties.ToLogProperties();
            Log(eventName, message, propertiesDictionary, level, exception);
        }

        /// <summary>
        /// Write an entry to Application Insights trace table, with optional properties and log level. If exception is provided, an entry is also written
        /// to the Exceptions table.
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="message"></param>
        /// <param name="properties"></param>
        /// <param name="level"></param>
        /// <param name="exception"></param>
        public void Log(string eventName, string message, IDictionary<string, string> properties = null, LogLevel level = LogLevel.Information, Exception exception = null)
        {
            if (string.IsNullOrWhiteSpace(eventName))
            {
                throw new ArgumentNullException(nameof(eventName));
            }

            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException(nameof(message));
            }

            string Formatter(Dictionary<string, object> props, Exception ex) => message;
            var dimensions = properties?.ToDictionary(p => p.Key, p => (object)p.Value);
            var eventId = new EventId(1, eventName);

            if (exception != null)
            {
                // this logs the full stack trace when logging to App Insights exceptions
                _logger.Log(level, eventId, dimensions, exception, Formatter);

                if (dimensions == null)
                {
                    dimensions = new Dictionary<string, object>();
                }

                // add the exception summary for the Log statement below
                dimensions[nameof(exception)] = exception.ToString();
            }

            // no need to pass the exception as we would have added an exception summary
            _logger.Log(level, eventId, dimensions, exception: null, Formatter);
        }

        /// <summary>
        /// Write an entry to the Application Insights customEvents table, with a loggable properties instance.
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="properties"></param>
        /// <param name="value"></param>
        public void TrackEvent(string eventName, object properties, double value = 1)
        {
            if (properties == null)
            {
                throw new ArgumentNullException(nameof(properties));
            }

            var propertiesDictionary = LoggingHelper.GetPropertiesWithAttribute(properties);
            TrackEvent(eventName, propertiesDictionary, value);
        }

        /// <summary>
        /// Write an entry to the Application Insights customEvents table, with a custom loggable properties instance.
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="properties"></param>
        /// <param name="value"></param>
        public void TrackEvent(string eventName, ICustomLoggable properties, double value = 1)
        {
            if (properties == null)
            {
                throw new ArgumentNullException(nameof(properties));
            }

            var propertiesDictionary = properties.ToLogProperties();
            TrackEvent(eventName, propertiesDictionary, value);
        }

        /// <summary>
        /// Write an entry to the Application Insights customEvents table, with optional properties.
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="properties"></param>
        /// <param name="value"></param>
        public void TrackEvent(string eventName, IDictionary<string, string> properties = null, double value = 1)
        {
            var metrics = new Dictionary<string, double>
            {
                {nameof(value), value}
            };

            TrackEvent(eventName, metrics, properties);
        }

        /// <summary>
        /// Write an entry to the Application Insights customEvents table, with a loggable properties instance.
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="metrics"></param>
        /// <param name="properties"></param>
        public void TrackEvent(string eventName, IDictionary<string, double> metrics, object properties)
        {
            if (properties == null)
            {
                throw new ArgumentNullException(nameof(properties));
            }

            var propertiesDictionary = LoggingHelper.GetPropertiesWithAttribute(properties);
            TrackEvent(eventName, metrics, propertiesDictionary);
        }

        /// <summary>
        /// Write an entry to the Application Insights customEvents table, with a custom loggable properties instance.
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="metrics"></param>
        /// <param name="properties"></param>
        public void TrackEvent(string eventName, IDictionary<string, double> metrics, ICustomLoggable properties)
        {
            if (properties == null)
            {
                throw new ArgumentNullException(nameof(properties));
            }

            var propertiesDictionary = properties.ToLogProperties();
            TrackEvent(eventName, metrics, propertiesDictionary);
        }

        /// <summary>
        /// Write an entry to the Application Insights customEvents table, with optional properties.
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="metrics"></param>
        /// <param name="properties"></param>
        public void TrackEvent(string eventName, IDictionary<string, double> metrics, IDictionary<string, string> properties = null)
        {
            if (string.IsNullOrWhiteSpace(eventName))
            {
                throw new ArgumentNullException(nameof(eventName));
            }

            if (metrics == null)
            {
                throw new ArgumentNullException(nameof(metrics));
            }

            _telemetryClient.TrackEvent(eventName, properties, metrics);
        }
    }
}
