using System;

namespace Hel.ECS.Exceptions
{
    public class InvalidWorldException : Exception
    {
        public InvalidWorldException(){}
        
        public InvalidWorldException(string msg) : base(msg) {}
        
    }
}