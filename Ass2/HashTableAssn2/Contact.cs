using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
namespace Assn2
{
    /*  Modifications from assignment 1:
     *      - EmployeeID field added
     *      - new input file generated, with 500 entries
     *      - Updated ReadContactsFromFile() and Print()
    *          with new EmployeeID field
    */
    public class Contact
    {
        private string _email = "";

        #region Constructors
        public Contact(string employeeID, string firstname, string lastname, string street,
        string city, string state, string zip, string phone, string email)
        {
            this.EmployeeID = employeeID;
            this.FirstName = firstname;
            this.LastName = lastname;
            this.Street = street;
            this.City = city;
            this.State = state;
            this.Zip = zip;
            this.Phone = phone;
            this.Email = email;
        }
        #endregion
        #region Properties
        public string EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string Email
        {
            get { return _email; }
            set
            {
                if (IsValidEmail(value))
                    _email = value;
                else
                {
                    Console.WriteLine($"{value}");
                    throw new ArgumentException("Invalid email address");
                }
            }
        }
        #endregion
        #region Methods
        public static List<Contact> ReadContactsFromFile(string path)
        {
            List<Contact> contacts = new List<Contact>();

            try
            {
                string[] lines = File.ReadAllLines(path);

                foreach (string line in lines)
                {
                    string[] values = line.Split(',');
                    Contact contact = new Contact(
                        values[0],
                        values[1],
                        values[2],
                        values[3],
                        values[4],
                        values[5],
                        values[6],
                        values[7],
                        values[8]
                    );

                    contacts.Add(contact);
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error reading the csv file: {0}", ex.Message);
            }
            return contacts;
        }

        public static void PrintAll(List<Contact> contacts)
        {
            foreach (Contact contact in contacts)
            {
                Print(contact);
            }
        }

        private static void Print(Contact contact)
        {
            Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}",
                                contact.EmployeeID,
                                contact.FirstName,
                                contact.LastName,
                                contact.Street,
                                contact.City,
                                contact.State,
                                contact.Zip,
                                contact.Phone,
                                contact.Email);
        }

        public static List<Contact> SortByLast(List<Contact> contacts)
        {
            contacts = (from c in contacts
                        orderby c.LastName
                        select c).ToList();
            return contacts;
        }

        public static void PrintEveryNum(List<Contact> contacts, int skip)
        {
            int i;
            for (i = skip - 1; i <= contacts.Count() - 1; i += skip)
            {
                Print(contacts[i]);
            }
        }

        private bool IsValidEmail(string email)
        {
            // Regular expression for basic email validation
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }
        #endregion
    }
}