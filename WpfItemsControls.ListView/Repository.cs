
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WpfItemsControls.ListView
{
    class Repository
    {
        private List<Employee> employees;
        private string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Employees.txt");

        public Repository()
        {
            employees = new List<Employee>();

            LoadFromFile(path);
        }

        public List<Employee> GetAll()
        {
            return employees;
        }

        public void AddToFile(Employee employee)
        {
            SaveToFile(employee);
        }

        public void ClearFile()
        {
            // StreamWriter for writing to file
            StreamWriter file = new StreamWriter(path, false);
            // Write Persons to file
            file.WriteLine();
            // Close file process
            file.Close();
        }

        private void SaveToFile(Employee employee)
        {
            try
            {
                // Convert employee to correct format used in the text file.
                string employeeToText =
                    $"{employee.Firstname}," +
                    $"{employee.Lastname}," +
                    $"{employee.Position}," +
                    $"{employee.Salary}," +
                    $"{employee.EmploymentDate.ToString("yyyy, MM, dd")}";

                // StreamWriter for writing to file
                StreamWriter file = new StreamWriter(path, true);
                // Write Persons to file
                file.WriteLine(employeeToText);
                // Close file process
                file.Close();
            }
            catch(System.IO.IOException)
            {
                // Prevents crash if file is being used by another process
            }
        }

        private void LoadFromFile(string filePath)
        {
            if(!File.Exists(filePath))
            {
                // Add default Employees to list
                employees.Add(new Employee("Lars", "Larsen", "Direktør", 297364, DateTime.Now));
                employees.Add(new Employee("Hans", "Hansen", "Marketingschef", 2863453, DateTime.Now));
                employees.Add(new Employee("Jens", "Jens", "Salgsdirektør", 297364, DateTime.Now));

                // Add default employees to file
                AddToFile(new Employee("Lars", "Larsen", "Direktør", 297364, DateTime.Now));
                AddToFile(new Employee("Hans", "Hansen", "Marketingschef", 2863453, DateTime.Now));
                AddToFile(new Employee("Jens", "Jens", "Salgsdirektør", 297364, DateTime.Now));
            }
            else
            {
                try
                {
                    // StreamReader for reading the document file
                    using(StreamReader reader = new StreamReader(filePath, true))
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

                                    // TryParse fourth line to int
                                    int.TryParse(lineArray[3], out int lineArrayInt);

                                    // Create Employee object and add to list
                                    //
                                    // Format: Firstname, Lastname, Position, Salary, Employment Year, Employment Day, Employment Month
                                    employees.Add(new Employee(
                                        lineArray[0],
                                        lineArray[1],
                                        lineArray[2],
                                        lineArrayInt,
                                        new DateTime(
                                            Convert.ToInt32(lineArray[4]),
                                            Convert.ToInt32(lineArray[5]),
                                            Convert.ToInt32(lineArray[6]))));
                                }
                                catch(IndexOutOfRangeException)
                                {
                                    // Catches any empty lines in the file
                                }
                                catch(System.ArgumentOutOfRangeException)
                                {
                                    // Catches error in DateTime formatting
                                }
                            }
                        }
                    }
                }
                catch(System.IO.IOException)
                {
                    // Catches error if the document is being used by another process
                }
            }
        }
    }
}