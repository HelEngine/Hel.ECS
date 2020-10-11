using System;

namespace Hel.ECS.Exceptions
{
    public class InvalidComponentException : Exception
    {
        public  InvalidComponentException() {}
        public InvalidComponentException(string msg) : base(msg) {}
    }
}