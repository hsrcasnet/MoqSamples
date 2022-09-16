using System.Diagnostics;
using System.Linq;

namespace MoqSamples.Office
{
    public class EmployeeManager
    {
        private readonly IDateTime dateTime;
        private readonly IPersonRepository personRepository;

        public EmployeeManager(IDateTime dateTime, IPersonRepository personRepository)
        {
            this.dateTime = dateTime;
            this.personRepository = personRepository;
        }

        public void DeleteRetiredPersons(int age)
        {
            var persons = this.personRepository.GetPersons();

            var now = this.dateTime.Now;

            var retiredPersons = persons
                .Where(p => p.CalculateAge(now) >= age)
                .ToList();

            foreach (var retiredPerson in retiredPersons)
            {
                Debug.WriteLine($"Deleting person '{retiredPerson.Name}'...");
                this.personRepository.DeletePerson(retiredPerson);
            }
        }
    }
}