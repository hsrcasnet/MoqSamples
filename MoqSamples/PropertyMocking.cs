using System.Diagnostics;
using System.Linq;

namespace MoqSamples
{
    public class PropertyMocking
    {
        private readonly IDateTime dateTime;
        private readonly IPersonRepository personRepository;

        public PropertyMocking(IDateTime dateTime, IPersonRepository personRepository)
        {
            this.dateTime = dateTime;
            this.personRepository = personRepository;
        }

        public void DeleteRetiredPersons(int age)
        {
            var persons = this.personRepository.GetPersons();

            var retiredPersons = persons
                .Where(p => p.CalculateAge(this.dateTime.Now) >= age)
                .ToList();

            foreach (var retiredPerson in retiredPersons)
            {
                Debug.WriteLine($"Deleting person '{retiredPerson.Name}'...");
                this.personRepository.DeletePerson(retiredPerson);
            }
        }
    }
}