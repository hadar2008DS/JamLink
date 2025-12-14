using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class PersonList : List<Person>
    {
        public PersonList() { }
        public PersonList(IEnumerable<Person> persons) : base(persons) { }
        public PersonList(IEnumerable<BaseEntity> persons) : base(persons.Cast<Person>().ToList()) { }
    }
}
