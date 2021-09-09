using System;

namespace MoqSamples.Kitchen
{
    public interface IThermostat
    {
        event EventHandler<ThermostatEventArgs> TooHot;

        event EventHandler<ThermostatEventArgs> TooCold;
    }
}