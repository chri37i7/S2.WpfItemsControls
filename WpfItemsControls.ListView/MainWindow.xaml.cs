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
        private Repository repository = new Repository();

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
                if(textBoxEmployeeFirstname.Text == "" || textBoxEmployeeLastname.Text == "" || textBoxEmployeePosition.Text == "" || textBoxEmployeeSalary.Text == "" || datePickerEmploymentDate.SelectedDate == null)
                {
                    if(viewModel.SelectedEmployee == null)
                    {
                        MessageBox.Show("Udfyld venligst felterne", "Fejl!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    try
                    {
                        DateTime selectedDate = datePickerEmploymentDate.SelectedDate ?? DateTime.Now;

                        Employee employee = new Employee(
                            textBoxEmployeeFirstname.Text,
                            textBoxEmployeeLastname.Text,
                            textBoxEmployeePosition.Text,
                            Convert.ToInt32(textBoxEmployeeSalary.Text),
                            selectedDate);

                        repository.AddToFile(employee);
                        viewModel.Employees.Add(employee);
                        listViewEmployees.SelectedItem = employee;
                    }
                    catch(System.FormatException error)
                    {

                        MessageBox.Show($"{error}", "Fejl!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else if(viewModel.SelectedEmployee != null)
            {
                if(textBoxEmployeeFirstname.Text == "" || textBoxEmployeeLastname.Text == "" || textBoxEmployeePosition.Text == "" || textBoxEmployeeSalary.Text == "" || datePickerEmploymentDate.SelectedDate == null)
                {
                    if(viewModel.SelectedEmployee == null)
                    {
                        MessageBox.Show("Udfyld venligst felterne", "Fejl!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    try
                    {
                        repository.ClearFile();

                        Employee employee = new Employee(
                            viewModel.SelectedEmployee.Firstname,
                            viewModel.SelectedEmployee.Lastname,
                            viewModel.SelectedEmployee.Position,
                            viewModel.SelectedEmployee.Salary,
                            viewModel.SelectedEmployee.EmploymentDate);

                        viewModel.Employees.Remove(viewModel.SelectedEmployee);

                        foreach(Employee addEmployee in viewModel.Employees)
                        {
                            repository.AddToFile(addEmployee);
                        }

                        repository.AddToFile(employee);
                        viewModel.Employees.Add(employee);
                        listViewEmployees.SelectedItem = employee;
                    }
                    catch(System.FormatException error)
                    {

                        MessageBox.Show($"{error.Message}", "Error Saving Employee", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void ButtonDeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            if(viewModel.SelectedEmployee != null)
            {
                viewModel.Employees.Remove(viewModel.SelectedEmployee);

                repository.ClearFile();

                foreach(Employee employee in viewModel.Employees)
                {
                    repository.AddToFile(employee);
                }
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