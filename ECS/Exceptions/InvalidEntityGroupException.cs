using System;

namespace Hel.ECS.Exceptions
{
    public class InvalidEntityGroupException  : Exception
    {
        public  InvalidEntityGroupException() {}
        public InvalidEntityGroupException(string msg) : base(msg) {}
    }
}