using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace S2.WpfItemsControls.ComboBox
{
    class ViewModel
    {
        private Repository repository;

        public ViewModel()
        {
            repository = new Repository();

            List<Movie> movies = repository.GetAll();

            Movies = new ObservableCollection<Movie>(movies);
        }

        public ObservableCollection<Movie> Movies { get; set; }

        public Movie SelectedMovie { get; set; }
    }
}
