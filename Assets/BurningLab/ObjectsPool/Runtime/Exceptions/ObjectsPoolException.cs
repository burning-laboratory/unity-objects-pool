using System;

namespace BurningLab.ObjectsPool.Exceptions
{
    /// <summary>
    /// Pool implementation exceptions.
    /// </summary>
    public class ObjectsPoolException : Exception
    {
        public ObjectsPoolException(string message) : base(message) {}
    }
}