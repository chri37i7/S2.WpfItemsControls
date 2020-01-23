using System;
using System.Collections.Generic;
using System.Text;

namespace S2.WpfItemsControls.ListBox
{
    public class Person
    {
        // Fields
        private string firstname;
        private string lastname;
        private string email;
        private int phoneNumber;

        // Contructor
        public Person(string firstname, string lastname, string email, int phoneNumber)
        {
            Firstname = firstname;
            Lastname = lastname;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        // Properties
        public string Firstname
        {
            get
            {
                return firstname;
            }
            set
            {
                firstname = value;
            }
        }

        public string Lastname
        {
            get
            {
                return lastname;
            }
            set
            {
                lastname = value;
            }
        }

        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
            }
        }

        public int PhoneNumber
        {
            get
            {
                return phoneNumber;
            }
            set
            {
                phoneNumber = value;
            }
        }

        public string Fullname => $"{Firstname} {Lastname}";
    }
}
