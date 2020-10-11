using System;

namespace Hel.ECS.Exceptions
{
    public class InvalidEntityException : Exception
    {
        public  InvalidEntityException() {}
        public InvalidEntityException(string msg) : base(msg) {}
    }
}