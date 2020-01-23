using System;
using System.Collections.Generic;
using System.Text;

namespace S2.WpfItemsControls.ListBox
{
    public class Repository
    {
        private List<Person> persons;

        public Repository()
        {
            persons = new List<Person>() {
                 new Person("Tony", "Cox", "cox@gmail.dk", 69133769),
                 new Person("Rick", "Titball", "titball@gmail.dk", 69696969),
                 new Person("Ben", "Dover", "dover@gmail.dk", 69420420),
                 new Person("Mike", "Litoris", "litoris@gmail.dk", 74839625),
                 new Person("Moe", "Lester", "lester@gmail.dk", 26837403)
            };
        }

        public List<Person> GetAll()
        {
            return persons;
        }

        public void Add(Person person)
        {
            persons.Add(person);
        }
    }
}
