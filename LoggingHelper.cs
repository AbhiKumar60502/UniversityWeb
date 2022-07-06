using System;
using System.Collections.Generic;
using System.Linq;

namespace Shm.Logging
{
    public class LoggingHelper
    {
        /// <summary>
        /// Return a dictionary of loggable properties given an object instance that has properties with the LoggableEntityAttribute
        /// associated
        /// </summary>
        /// <param name="properties">Object instance with LoggableEntity properties</param>
        public static Dictionary<string, string> GetPropertiesWithAttribute(object properties)
        {
            if (properties == null)
            {
                throw new ArgumentNullException(nameof(properties));
            }
            
            var typeOfLoggable = properties.GetType();
            var loggableEntityAttribute = Attribute.GetCustomAttribute(typeOfLoggable, typeof (LoggableEntityAttribute));

            if (loggableEntityAttribute == null)
            {
                throw new ArgumentException("No LoggableEntity attribute found on properties object instance");
            }

            var propertiesDictionary = typeOfLoggable.GetProperties()
                .Where(x => Attribute.IsDefined(x, typeof(LoggableAttribute)))
                .ToDictionary(p => p.Name, p => p.GetValue(properties, null)?.ToString());

            return propertiesDictionary;
        }
    }
}