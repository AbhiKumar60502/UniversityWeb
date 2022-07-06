using System;

namespace Shm.Logging.ApplicationInsights
{
    /// <summary>
    /// ScopedApplicationLogger supports the logical grouping of a set of logging operations. The given constructor properties are
    /// mapped to customDimensions for every logging call with the scope.
    /// </summary>
    public interface IScopedApplicationLogger : IApplicationLogger, IDisposable
    {
    }
}