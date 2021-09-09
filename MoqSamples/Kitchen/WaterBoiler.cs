using System;
using System.Diagnostics;

namespace MoqSamples.Kitchen
{
    public class WaterBoiler : IDisposable
    {
        private readonly IThermostat thermostat;
        private readonly IHeater heater;

        public WaterBoiler(IThermostat thermostat, IHeater heater)
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
}