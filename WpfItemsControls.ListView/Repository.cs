
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

        public void Add(Employee employee)
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

        private void LoadFromFile(string filePath)
        {
            if(!File.Exists(filePath))
            {
                // Create the file
                File.Create(filePath);

                // Add empty Employee
                employees.Add(new Employee(default, default, default, default, default));
            }
            else
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
                        }
                    }
                }
            }
        }


    }
}