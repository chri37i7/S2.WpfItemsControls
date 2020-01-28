﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WpfItemsControls.ListView
{
    class Employee
    {
        // Constructor
        public Employee(string firstname, string lastname, string position, int sallery, DateTime employmentDate)
        {
            Firstname = firstname;
            Lastname = lastname;
            Position = position;
            Sallery = sallery;
            EmploymentDate = employmentDate;
        }

        // Properties
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Position { get; set; }
        public int Sallery { get; set; }
        public DateTime EmploymentDate { get; set; }
        public string Fullname => $"{Firstname} {Lastname}";
    }
}