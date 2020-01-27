
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
        private string buttonState;

        public MainWindow()
        {
            InitializeComponent();
            viewModel = new ViewModel();
            DataContext = viewModel;
        }

        private void Button_AddPerson_Click(object sender, RoutedEventArgs e)
        {
            if(textBox_Firstname.Text == "" || textBox_Lastname.Text == "" || textBox_Email.Text == "" || textBox_Phone.Text == "")
            {
                MessageBox.Show("Udfyld venligst felterne", "Fejl!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                int.TryParse(textBox_Phone.Text, out int phone);

                Person person = new Person(
                    textBox_Firstname.Text,
                    textBox_Lastname.Text,
                    textBox_Email.Text,
                    phone);

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

        private void ButtonEditPerson_Click(object sender, RoutedEventArgs e)
        {
            // Statements for multi-functional button.
            if(buttonState == null)
            {
                buttonState = "1";

                buttonEditPerson.Content = "Save";

                if(personsListBox.SelectedItem != null)
                {
                    TextBoxWriteable();
                }
            }
            else if(buttonState == "1")
            {
                buttonState = null;
                buttonEditPerson.Content = "Edit";

                int.TryParse(textBoxInfo_PhoneNumber.Text, out int phoneNumber);

                if(viewModel.SelectedPerson != null)
                {
                    if(phoneNumber != 0)
                    {
                        Person editedPerson = new Person(
                            textBoxInfo_Firstname.Text,
                            textBoxInfo_Lastname.Text,
                            textBoxInfo_Email.Text,
                            phoneNumber);

                        viewModel.Persons.Remove(viewModel.SelectedPerson);

                        viewModel.Persons.Add(editedPerson);
                    }
                    else
                    {
                        MessageBox.Show("Indtast venligst et gyldigt telefonnummer", "Fejl!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                TextBoxReadOnly();
            }
        }

        private void Button_DeletePerson_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Persons.Remove(viewModel.SelectedPerson);
        }

        private void Button_ImportPersons_Click(object sender, RoutedEventArgs e)
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

        private void TextBoxWriteable()
        {
            textBoxInfo_Firstname.IsReadOnly = false;
            textBoxInfo_Firstname.BorderThickness = new System.Windows.Thickness(1);

            textBoxInfo_Lastname.IsReadOnly = false;
            textBoxInfo_Lastname.BorderThickness = new System.Windows.Thickness(1);

            textBoxInfo_Email.IsReadOnly = false;
            textBoxInfo_Email.BorderThickness = new System.Windows.Thickness(1);

            textBoxInfo_PhoneNumber.IsReadOnly = false;
            textBoxInfo_PhoneNumber.BorderThickness = new System.Windows.Thickness(1);
        }

        private void TextBoxReadOnly()
        {
            textBoxInfo_Firstname.IsReadOnly = true;
            textBoxInfo_Firstname.BorderThickness = new System.Windows.Thickness(0);

            textBoxInfo_Lastname.IsReadOnly = true;
            textBoxInfo_Lastname.BorderThickness = new System.Windows.Thickness(0);

            textBoxInfo_Email.IsReadOnly = true;
            textBoxInfo_Email.BorderThickness = new System.Windows.Thickness(0);

            textBoxInfo_PhoneNumber.IsReadOnly = true;
            textBoxInfo_PhoneNumber.BorderThickness = new System.Windows.Thickness(0);
        }
    }
}