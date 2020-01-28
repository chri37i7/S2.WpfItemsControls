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

        private void ButtonNewEmployee_Click(object sender, RoutedEventArgs e)
        {
            if(textBoxEmployeeFirstname.IsReadOnly != true)
            {
                MessageBox.Show("Husk at tryk gem.", "Fejl!", MessageBoxButton.OK, MessageBoxImage.Error);
            }        
            else
            {
                // Deselect SelectedItem
                listViewEmployees.SelectedItem = null;

                // Set TextBox BorderThickness
                datePickerEmploymentDate.BorderThickness = new System.Windows.Thickness(1);
                textBoxEmployeeFirstname.BorderThickness = new System.Windows.Thickness(1);
                textBoxEmployeeLastname.BorderThickness = new System.Windows.Thickness(1);
                textBoxEmployeePosition.BorderThickness = new System.Windows.Thickness(1);
                textBoxEmployeeSalary.BorderThickness = new System.Windows.Thickness(1);

                // Make TextBoxes Writeable
                textBoxEmployeeFirstname.IsReadOnly = false;
                textBoxEmployeeLastname.IsReadOnly = false;
                textBoxEmployeePosition.IsReadOnly = false;
                textBoxEmployeeSalary.IsReadOnly = false;

                // Enable DatePicker
                datePickerEmploymentDate.IsEnabled = true;
            }
        }

        private void ButtonEditEmployee_Click(object sender, RoutedEventArgs e)
        {
            if(viewModel.SelectedEmployee != null)
            {
                // Set TextBox BorderThickness
                datePickerEmploymentDate.BorderThickness = new System.Windows.Thickness(1);
                textBoxEmployeeFirstname.BorderThickness = new System.Windows.Thickness(1);
                textBoxEmployeeLastname.BorderThickness = new System.Windows.Thickness(1);
                textBoxEmployeePosition.BorderThickness = new System.Windows.Thickness(1);
                textBoxEmployeeSalary.BorderThickness = new System.Windows.Thickness(1);

                // Make TextBoxes Writeable
                textBoxEmployeeFirstname.IsReadOnly = false;
                textBoxEmployeeLastname.IsReadOnly = false;
                textBoxEmployeePosition.IsReadOnly = false;
                textBoxEmployeeSalary.IsReadOnly = false;

                // Enable DatePicker
                datePickerEmploymentDate.IsEnabled = true;
            }
        }

        private void ButtonSaveEmployee_Click(object sender, RoutedEventArgs e)
        {
            if(viewModel.SelectedEmployee == null)
            {
                if(textBoxEmployeeFirstname.Text == "" || textBoxEmployeeLastname.Text == "" || textBoxEmployeePosition.Text == "" || datePickerEmploymentDate.SelectedDate == null)
                {
                    if(viewModel.SelectedEmployee == null)
                    {
                        MessageBox.Show("Udfyld venligst felterne", "Fejl!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    Employee employee = new Employee(
                        textBoxEmployeeFirstname.Text,
                        textBoxEmployeeLastname.Text,
                        textBoxEmployeePosition.Text,
                        Convert.ToInt32(textBoxEmployeeSalary.Text),
                        (datePickerEmploymentDate.SelectedDate ?? DateTime.Now));

                    viewModel.Employees.Add(employee);
                    listViewEmployees.SelectedItem = employee;
                }
            }
            else if(viewModel.SelectedEmployee != null)
            {
                if(textBoxEmployeeFirstname.Text == "" || textBoxEmployeeLastname.Text == "" || textBoxEmployeePosition.Text == "" || datePickerEmploymentDate.SelectedDate == null)
                {
                    if(viewModel.SelectedEmployee == null)
                    {
                        MessageBox.Show("Udfyld venligst felterne", "Fejl!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    Employee employee = new Employee(
                        textBoxEmployeeFirstname.Text,
                        textBoxEmployeeLastname.Text,
                        textBoxEmployeePosition.Text,
                        Convert.ToInt32(textBoxEmployeeSalary.Text),
                        (datePickerEmploymentDate.SelectedDate ?? DateTime.Now));

                    viewModel.Employees.Remove(viewModel.SelectedEmployee);
                    viewModel.Employees.Add(employee);
                    listViewEmployees.SelectedItem = employee;
                }
            }
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

        private void ListViewEmployees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Set BorderThicknesses
            datePickerEmploymentDate.BorderThickness = new System.Windows.Thickness(0);
            textBoxEmployeeFirstname.BorderThickness = new System.Windows.Thickness(0);
            textBoxEmployeeLastname.BorderThickness = new System.Windows.Thickness(0);
            textBoxEmployeePosition.BorderThickness = new System.Windows.Thickness(0);
            textBoxEmployeeSalary.BorderThickness = new System.Windows.Thickness(0);

            // Make TextBoxes Writeable
            textBoxEmployeeFirstname.IsReadOnly = true;
            textBoxEmployeeLastname.IsReadOnly = true;
            textBoxEmployeePosition.IsReadOnly = true;
            textBoxEmployeeSalary.IsReadOnly = true;

            // Disable DatePicker
            datePickerEmploymentDate.IsEnabled = false;
        }
    }
}
