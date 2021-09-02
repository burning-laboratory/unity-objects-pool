using System;

namespace GoToApps.ObjectsPool.Exceptions
{
    /// <summary>
    /// Pool implementation exceptions.
    /// </summary>
    public class ObjectsPoolException : Exception
    {
        public ObjectsPoolException(string message) : base(message) {}
    }
}