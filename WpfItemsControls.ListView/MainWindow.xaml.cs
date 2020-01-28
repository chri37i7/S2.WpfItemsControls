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

namespace WpfItemsControls.ListView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModel viewModel;

        public MainWindow()
        {
            InitializeComponent();

            viewModel = new ViewModel();
            DataContext = viewModel;
        }

        private void ButtonDeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            if(viewModel.SelectedEmployee != null)
            {
                viewModel.Employees.Remove(viewModel.SelectedEmployee);
            }
            else
            {
                MessageBox.Show("Vælg venligst en ansat", "Ingen ansat valgt", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ButtonNewEmployee_Click(object sender, RoutedEventArgs e)
        {
            if(viewModel.SelectedEmployee == null)
            {

            }
            else
            {
                // Deselect SelectedItem
                listViewEmployees.SelectedItem = null;

                // Set TextBox BorderThickness
                textBoxEmployeeFirstname.BorderThickness = new System.Windows.Thickness(1);
                textBoxEmployeeLastname.BorderThickness = new System.Windows.Thickness(1);
                textBoxEmployeePosition.BorderThickness = new System.Windows.Thickness(1);

                // Make TextBoxes Writeable
                textBoxEmployeeFirstname.IsReadOnly = false;
                textBoxEmployeeLastname.IsReadOnly = false;
                textBoxEmployeePosition.IsReadOnly = false;
            }
        }

        private void ButtonEditEmployee_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
