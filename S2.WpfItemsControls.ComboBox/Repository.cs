using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace S2.WpfItemsControls.ComboBox
{


    class Repository
    {
        private List<Movie> movies;
        private string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "movies.txt");

        public Repository()
        {
            movies = new List<Movie>();

            LoadFromFile(path);
        }

        public List<Movie> GetAll()
        {
            return movies;
        }

        public void SaveToFile(Movie movie)
        {
            try
            {
                // Convert employee to correct format used in the text file.
                string movieToText =
                    $"{movie.Title}," +
                    $"{movie.Genre}," +
                    $"{movie.LeadActor}," +
                    $"{movie.Playtime}," +
                    $"{movie.ReleaseDate.ToString("yyyy,MM,dd")}";

                // StreamWriter for writing to file
                StreamWriter file = new StreamWriter(path, true);
                // Write Persons to file
                file.WriteLine(movieToText);
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
                movies.Add(new Movie("The Rise of Skywalker", "Science-fiction", "Daisy Ridley", 141, new DateTime(2019, 12, 18)));

                // Add default employees to file
                SaveToFile(new Movie("The Rise of Skywalker", "Science-fiction", "Daisy Ridley", 141, new DateTime(2019, 12, 18)));
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
                                    movies.Add(new Movie(
                                        lineArray[0],
                                        lineArray[1],
                                        lineArray[2],
                                        lineArrayInt,
                                        new DateTime(
                                            Convert.ToInt32(lineArray[4]),
                                            Convert.ToInt32(lineArray[5]),
                                            Convert.ToInt32(lineArray[6]))));
                                }
                                catch(System.FormatException)
                                {
                                    // Catches DateTime format exception
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