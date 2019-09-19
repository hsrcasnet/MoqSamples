﻿using System;
using System.Diagnostics;
using System.Linq;

namespace MoqSamples
{
    /// <summary>
    /// This could be a heater or a toaster.
    /// </summary>
    public class EventMocking : IDisposable
    {
        private readonly IThermostat thermostat;
        private readonly IHeater heater;

        public EventMocking(IThermostat thermostat, IHeater heater)
        {
            this.thermostat = thermostat;
            this.heater = heater;

            this.thermostat.TooHot += this.OnThermostatTooHot;
            this.thermostat.TooCold += this.OnThermostatTooCold;
        }

        private void OnThermostatTooHot(object sender, ThermostatEventArgs e)
        {
            Debug.WriteLine($"OnThermostatTooHot: Temperature={e.Temperature}°C");
            this.OnOffCycles++;
            this.heater.TurnOff();
        }
        
        private void OnThermostatTooCold(object sender, ThermostatEventArgs e)
        {
            Debug.WriteLine($"OnThermostatTooCold: Temperature={e.Temperature}°C");
            this.OnOffCycles++;
            this.heater.TurnOn();
        }

        public int OnOffCycles { get; set; }

        public void Dispose()
        {
            this.thermostat.TooHot -= this.OnThermostatTooHot;
            this.thermostat.TooCold -= this.OnThermostatTooCold;
        }
    }

    public interface IHeater
    {
        void TurnOn();

        void TurnOff();
    }

    public interface IThermostat
    {
        event EventHandler<ThermostatEventArgs> TooHot;
        event EventHandler<ThermostatEventArgs> TooCold;
    }

    public class ThermostatEventArgs : EventArgs
    {
        public ThermostatEventArgs(decimal temperature)
        {
            this.Temperature = temperature;
        }

        public decimal Temperature { get; }
    }
}