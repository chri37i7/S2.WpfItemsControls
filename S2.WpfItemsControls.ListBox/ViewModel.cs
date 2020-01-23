using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace S2.WpfItemsControls.ListBox
{
    class ViewModel
    {
        private Repository repository;

        public ViewModel()
        {
            repository = new Repository();

            List<Person> persons = repository.GetAll();

            Persons = new ObservableCollection<Person>(persons);
        }

        public ObservableCollection<Person> Persons { get; set; }

        public Person SelectedPerson { get; set; }
    }
}
