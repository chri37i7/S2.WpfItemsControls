using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace S2.WpfItemsControls.ComboBox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModel viewModel;
        private Repository repository;

        public MainWindow()
        {
            InitializeComponent();

            repository = new Repository();
            viewModel = new ViewModel();
            DataContext = viewModel;
        }

        private void ButtonEditMovie_Click(object sender, RoutedEventArgs e)
        {
            if(viewModel.SelectedMovie != null)
            {
                // Change border thickness on TextBoxes
                textBoxMovieTitle.BorderThickness = new System.Windows.Thickness(1);
                textBoxMovieGenre.BorderThickness = new System.Windows.Thickness(1);
                textBoxMovieLeadActor.BorderThickness = new System.Windows.Thickness(1);
                textBoxMoviePlaytime.BorderThickness = new System.Windows.Thickness(1);
                datePickerMovieReleaseDate.BorderThickness = new System.Windows.Thickness(1);

                // Enable TextBoxes & DatePicker
                textBoxMovieTitle.IsReadOnly = false;
                textBoxMovieGenre.IsReadOnly = false;
                textBoxMovieLeadActor.IsReadOnly = false;
                textBoxMoviePlaytime.IsReadOnly = false;
                datePickerMovieReleaseDate.IsEnabled = true;

                // Show TextBox
                textBoxMinutes.Opacity = 1;
            }
        }

        private void ButtonNewMovie_Click(object sender, RoutedEventArgs e)
        {
            // Deselect SelectedMovie
            listViewMovies.SelectedItem = null;

            // Change border thickness on TextBoxes
            textBoxMovieTitle.BorderThickness = new System.Windows.Thickness(1);
            textBoxMovieGenre.BorderThickness = new System.Windows.Thickness(1);
            textBoxMovieLeadActor.BorderThickness = new System.Windows.Thickness(1);
            textBoxMoviePlaytime.BorderThickness = new System.Windows.Thickness(1);
            datePickerMovieReleaseDate.BorderThickness = new System.Windows.Thickness(1);

            // Enable TextBoxes & DatePicker
            textBoxMovieTitle.IsReadOnly = false;
            textBoxMovieGenre.IsReadOnly = false;
            textBoxMovieLeadActor.IsReadOnly = false;
            textBoxMoviePlaytime.IsReadOnly = false;
            datePickerMovieReleaseDate.IsEnabled = true;

            // Show TextBox
            textBoxMinutes.Opacity = 1;
        }

        private void ListViewMovies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(viewModel.SelectedMovie != null)
            {
                // Change border thickness on TextBoxes
                textBoxMovieTitle.BorderThickness = new System.Windows.Thickness(0);
                textBoxMovieGenre.BorderThickness = new System.Windows.Thickness(0);
                textBoxMovieLeadActor.BorderThickness = new System.Windows.Thickness(0);
                textBoxMoviePlaytime.BorderThickness = new System.Windows.Thickness(0);
                datePickerMovieReleaseDate.BorderThickness = new System.Windows.Thickness(0);

                // Enable TextBoxes & DatePicker
                textBoxMovieTitle.IsReadOnly = true;
                textBoxMovieGenre.IsReadOnly = true;
                textBoxMovieLeadActor.IsReadOnly = true;
                textBoxMoviePlaytime.IsReadOnly = true;
                datePickerMovieReleaseDate.IsEnabled = false;

                // Hide TextBox
                textBoxMinutes.Opacity = 0;
            }
        }

        private void ButtonDeleteMovie_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Movies.Remove(viewModel.SelectedMovie);
        }
    }
}
