using System;

namespace MoqSamples.Garage
{
    public class EngineStartException : Exception
    {
        public EngineStartException(string message) : base(message)
        {
        }
    }
}