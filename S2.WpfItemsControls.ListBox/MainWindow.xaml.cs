
using System;
using System.IO;
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
using Microsoft.Win32;

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

        private void ButtonSavePerson_Click(object sender, RoutedEventArgs e)
        {
            // If a person in listBoxPersons is not selected
            //
            // Add Person Funtion
            if(viewModel.SelectedPerson == null)
            {
                // If any of the textboxes is not filled out
                if(textBoxFirstname.Text == "" || textBoxLastname.Text == "" || textBoxEmail.Text == "" || textBoxPhoneNumber.Text == "")
                {
                    MessageBox.Show("Udfyld venligst felterne", "Fejl!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                // If all textboxes is filled out
                else
                {
                    // Create person object
                    int.TryParse(textBoxPhoneNumber.Text, out int phone);
                    Person person = new Person(
                        textBoxFirstname.Text,
                        textBoxLastname.Text,
                        textBoxEmail.Text,
                        phone);

                    // Add person object to database
                    viewModel.Persons.Add(person);
                    // Set SelectedPerson to added person
                    listBoxPersons.SelectedItem = person;
                } 
            }
            // If a person in listBoxPersons is selected
            //
            // Edit Person Function
            else if(viewModel.SelectedPerson != null)
            {
                // Create person object
                int.TryParse(textBoxPhoneNumber.Text, out int phone);
                Person person = new Person(
                    textBoxFirstname.Text,
                    textBoxLastname.Text,
                    textBoxEmail.Text,
                    phone);

                // Delete SelectedPerson
                viewModel.Persons.Remove(viewModel.SelectedPerson);
                // Add new person
                viewModel.Persons.Add(person);
                // Set SelectedPerson to added person
                listBoxPersons.SelectedItem = person;
            }
        }

        public void ButtonSaveToFile_Click(object sender, RoutedEventArgs e)
        {
            // For dialog window to select file
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Filter = "Text Files(*.txt)|*.txt|All(*.*)|*",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            };

            if(saveFileDialog.ShowDialog() == true)
            {
                // List for storing Persons
                List<string> savePersonsToFile = new List<string>();

                // Add all from Persons list, to PersonsToFile list
                foreach(Person person in viewModel.Persons)
                {
                    // Information string
                    string personToText = $"{person.Firstname},{person.Lastname},{person.Email},{person.PhoneNumber}";

                    // Add string to list
                    savePersonsToFile.Add(personToText);
                }
                // StreamWriter for writing to file
                StreamWriter file = new StreamWriter(saveFileDialog.FileName);
                // Write each line to file
                foreach(string line in savePersonsToFile)
                {
                    // Write Persons to file
                    file.WriteLine(line);
                }
                // Close file
                file.Close();
            }
        }

        private void ButtonImportPersons_Click(object sender, RoutedEventArgs e)
        {
            // For filedialog
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "Text Files(*.txt)|*.txt|All(*.*)|*",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            };

            // Open dialog window if true
            if(openFileDialog.ShowDialog() == true)
            {
                // StreamReader for reading the document file
                using(StreamReader reader = new StreamReader(openFileDialog.FileName))
                {
                    // Read until end of the document
                    while(!reader.EndOfStream)
                    {
                        string documentLine;
                        // Read until end is reached
                        while((documentLine = reader.ReadLine()) != null)
                        {
                            try
                            {
                                // Split document lines into array
                                string[] lineArray = documentLine.Split(",");

                                // TryParse second line to int
                                int.TryParse(lineArray[3], out int lineArrayInt);

                                // Create Person object with array
                                Person person = new Person(lineArray[0], lineArray[1], lineArray[2], lineArrayInt);
                                // Add Person to Persons list
                                viewModel.Persons.Add(person);
                            }
                            catch(IndexOutOfRangeException)
                            {
                                MessageBox.Show("Der opsted en fejl ved indlæsning, check tekstfilen for mellemrum.");
                            }
                        }
                    }
                }
            }
        }

        private void ButtonDeletePerson_Click(object sender, RoutedEventArgs e)
        {
            // If a Person i listBoxPersons is selected
            if(viewModel.SelectedPerson != null)
            {
                // Delete Person in Persons list
                viewModel.Persons.Remove(viewModel.SelectedPerson);
            }
        }

        private void ButtonAddPerson_Click(object sender, RoutedEventArgs e)
        {
            // Deselect any SelectedPerson
            listBoxPersons.SelectedItem = null;

            // If no Person is selected in textBoxPersons
            if(viewModel.SelectedPerson == null)
            {
                // Reveal the save button
                buttonSavePerson.Opacity = 1;

                // Make TextBox Writeable
                textBoxFirstname.IsReadOnly = false;
                textBoxLastname.IsReadOnly = false;
                textBoxEmail.IsReadOnly = false;
                textBoxPhoneNumber.IsReadOnly = false;

                // Make Borderthickness 1px
                textBoxFirstname.BorderThickness = new System.Windows.Thickness(1);
                textBoxLastname.BorderThickness = new System.Windows.Thickness(1);
                textBoxEmail.BorderThickness = new System.Windows.Thickness(1);
                textBoxPhoneNumber.BorderThickness = new System.Windows.Thickness(1);

                // Change TextBox Width
                textBoxFirstname.Width = 150;
                textBoxLastname.Width = 150;
                textBoxEmail.Width = 150;
                textBoxPhoneNumber.Width = 112; 
            }
        }

        private void ButtonEditPerson_Click(object sender, RoutedEventArgs e)
        {
            if(viewModel.SelectedPerson != null)
            {
                // Reveal the save button
                buttonSavePerson.Opacity = 1;

                // Make TextBox Writeable
                textBoxFirstname.IsReadOnly = false;
                textBoxLastname.IsReadOnly = false;
                textBoxEmail.IsReadOnly = false;
                textBoxPhoneNumber.IsReadOnly = false;

                // Make Borderthickness 1px
                textBoxFirstname.BorderThickness = new System.Windows.Thickness(1);
                textBoxLastname.BorderThickness = new System.Windows.Thickness(1);
                textBoxEmail.BorderThickness = new System.Windows.Thickness(1);
                textBoxPhoneNumber.BorderThickness = new System.Windows.Thickness(1);

                // Change TextBox Width
                textBoxFirstname.Width = 150;
                textBoxLastname.Width = 150;
                textBoxEmail.Width = 150;
                textBoxPhoneNumber.Width = 112;
            }
        }

        private void ListBoxPersons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Hide the save button
            buttonSavePerson.Opacity = 0;

            // Make TextBox unwriteable
            textBoxFirstname.IsReadOnly = true;
            textBoxLastname.IsReadOnly = true;
            textBoxEmail.IsReadOnly = true;
            textBoxPhoneNumber.IsReadOnly = true;

            // Change TextBox Width
            textBoxFirstname.BorderThickness = new System.Windows.Thickness(0);
            textBoxLastname.BorderThickness = new System.Windows.Thickness(0);
            textBoxEmail.BorderThickness = new System.Windows.Thickness(0);
            textBoxPhoneNumber.BorderThickness = new System.Windows.Thickness(0);
        }
    }
}