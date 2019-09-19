using System.Collections.Generic;

namespace MoqSamples
{
    public interface IPersonRepository
    {
        IEnumerable<Person> GetPersons();

        void DeletePerson(Person person);
    }
}