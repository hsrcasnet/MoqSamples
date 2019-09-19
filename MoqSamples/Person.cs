using System;
using System.Diagnostics;

namespace MoqSamples
{
    [DebuggerDisplay("Person: Name={this.Name}")]
    public class Person
    {
        public string Name { get; set; }

        public DateTime Birthdate { get; set; }

        public int CalculateAge(DateTime now)
        {
            var birthDate = this.Birthdate;
            var age = now.Year - birthDate.Year;
            if (birthDate > now.AddYears(-age))
            {
                age--;
            }
            return age;
        }
    }
}