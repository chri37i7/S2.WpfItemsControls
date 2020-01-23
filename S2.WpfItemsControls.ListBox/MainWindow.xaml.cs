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

namespace S2.WpfItemsControls.ListBox
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

        private void PersonsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Person selectedPerson = personsListBox.SelectedItem as Person;

            viewModel.SelectedPerson = selectedPerson;
        }

        private void Button_AddPerson_Click(object sender, RoutedEventArgs e)
        {
            int.TryParse(textBox_Phone.Text, out int phone);

            Person person = new Person(
                $"{textBox_Firstname.Text}",
                $"{textBox_Lastname.Text}",
                $"{textBox_Email.Text}", 
                phone);

            viewModel.Persons.Add(person);
        }
    }
}