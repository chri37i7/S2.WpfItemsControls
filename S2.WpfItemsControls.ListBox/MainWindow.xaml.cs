
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
            if(viewModel.SelectedPerson == null)
            {
                if(textBoxFirstname.Text == "" || textBoxLastname.Text == "" || textBoxEmail.Text == "" || textBoxPhoneNumber.Text == "")
                {
                    MessageBox.Show("Udfyld venligst felterne", "Fejl!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    int.TryParse(textBoxPhoneNumber.Text, out int phone);

                    Person person = new Person(
                        textBoxFirstname.Text,
                        textBoxLastname.Text,
                        textBoxEmail.Text,
                        phone);

                    viewModel.Persons.Add(person);

                    listBoxPersons.SelectedItem = person;

                } 
            }
            else if(viewModel.SelectedPerson != null)
            {
                int.TryParse(textBoxPhoneNumber.Text, out int phone);

                Person person = new Person(
                    textBoxFirstname.Text,
                    textBoxLastname.Text,
                    textBoxEmail.Text,
                    phone);

                viewModel.Persons.Remove(viewModel.SelectedPerson);
                viewModel.Persons.Add(person);
            }
        }

        public void ButtonSaveToFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Filter = "Text Files(*.txt)|*.txt|All(*.*)|*",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            };

            if(saveFileDialog.ShowDialog() == true)
            {
                List<string> savePersonsToFile = new List<string>();

                foreach(Person person in viewModel.Persons)
                {
                    string personToText = $"{person.Firstname},{person.Lastname},{person.Email},{person.PhoneNumber}";

                    savePersonsToFile.Add(personToText);
                }

                StreamWriter file = new StreamWriter(saveFileDialog.FileName);
                foreach(string line in savePersonsToFile)
                {
                    file.WriteLine(line);
                }
                file.Close();
            }
        }

        private void ButtonImportPersons_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "Text Files(*.txt)|*.txt|All(*.*)|*",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            };

            if(openFileDialog.ShowDialog() == true)
            {
                using(StreamReader reader = new StreamReader(openFileDialog.FileName))
                {
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

                                // Create film object with array
                                Person person = new Person(lineArray[0], lineArray[1], lineArray[2], lineArrayInt);
                                // Add film to list
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

        private void ButtonAddPerson_Click(object sender, RoutedEventArgs e)
        {
            listBoxPersons.SelectedItem = null;

            if(viewModel.SelectedPerson == null)
            {
                groupBoxPersonInfo.Margin = new System.Windows.Thickness(25, 25, 25, -25);

                textBoxFirstname.IsReadOnly = false;
                textBoxLastname.IsReadOnly = false;
                textBoxEmail.IsReadOnly = false;
                textBoxPhoneNumber.IsReadOnly = false;

                textBoxFirstname.BorderThickness = new System.Windows.Thickness(1);
                textBoxLastname.BorderThickness = new System.Windows.Thickness(1);
                textBoxEmail.BorderThickness = new System.Windows.Thickness(1);
                textBoxPhoneNumber.BorderThickness = new System.Windows.Thickness(1);

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
                groupBoxPersonInfo.Margin = new System.Windows.Thickness(25, 25, 25, -25);

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


        private void ButtonDeletePerson_Click(object sender, RoutedEventArgs e)
        {
            if(viewModel.SelectedPerson != null)
            {
                viewModel.Persons.Remove(viewModel.SelectedPerson); 
            }
        }

        private void ListBoxPersons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            groupBoxPersonInfo.Margin = new System.Windows.Thickness(25);

            textBoxFirstname.IsReadOnly = true;
            textBoxLastname.IsReadOnly = true;
            textBoxEmail.IsReadOnly = true;
            textBoxPhoneNumber.IsReadOnly = true;

            textBoxFirstname.BorderThickness = new System.Windows.Thickness(0);
            textBoxLastname.BorderThickness = new System.Windows.Thickness(0);
            textBoxEmail.BorderThickness = new System.Windows.Thickness(0);
            textBoxPhoneNumber.BorderThickness = new System.Windows.Thickness(0);
        }
    }
}