using System;
using System.Collections.Generic;
using System.Text;

namespace WpfItemsControls.ListView
{
    class Repository
    {
       private List<Employee> employees;

        public Repository()
        {
            employees = new List<Employee>()
            {
                new Employee("Brian", "Jørgensen", "Afdelingsleder", 250000, new DateTime(2013, 5, 3)),
                new Employee("Martin", "Fessel", "Lærer", 1000000, new DateTime(2016, 7, 13)),
                new Employee("Harry", "Hoe", "Pedel", 50, new DateTime(1947, 11, 23))
            };
        }

        public List<Employee> GetAll()
        {
            return employees;
        }

        public void add(Employee employee)
        {
            employees.Add(employee);
        }
    }
}
