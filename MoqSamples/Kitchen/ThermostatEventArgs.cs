using System;

namespace MoqSamples.Kitchen
{
    public class ThermostatEventArgs : EventArgs
    {
        public ThermostatEventArgs(decimal temperature)
        {
            this.Temperature = temperature;
        }

        public decimal Temperature { get; }
    }
}