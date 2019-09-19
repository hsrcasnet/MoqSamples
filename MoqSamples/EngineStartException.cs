using System;

namespace MoqSamples
{
    public class EngineStartException : Exception
    {
        public EngineStartException(string message) : base(message)
        {
        }
    }
}