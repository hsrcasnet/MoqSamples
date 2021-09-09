using System.Collections.Generic;

namespace MoqSamples.Office
{
    public interface IPersonRepository
    {
        IEnumerable<Person> GetPersons();

        void DeletePerson(Person person);
    }
}