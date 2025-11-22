using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
namespace Assn1
{
    class Program
    {
        public static void Main(string[] args)
        {
            // Keep track of total elapsed time
            Stopwatch totalTime = new Stopwatch();
            totalTime.Start();

            List<Contact> contact_list = new List<Contact>();
            
            string filePath1 = "../us-contacts.csv";
            string filePath2 = "../export.csv";

            // import from first file, sort by last name
            contact_list = Contact.ReadContactsFromFile(filePath1);
            contact_list = Contact.SortByLast(contact_list);

            Console.WriteLine($"\nContacts imported from {filePath1}, sorted by last name, printing every 50 contacts...\n");
            // print every 50 indices, starting at 49
            Contact.PrintEveryNum(contact_list, 50);

            // import from second file, sort by last name
            contact_list = Contact.ReadContactsFromFile(filePath2);
            contact_list = Contact.SortByLast(contact_list);

            Console.WriteLine($"\nContacts imported from {filePath2}, sorted by last name, printing every 1000 contacts...\n");
            // print every 1000 indices, starting at 999
            Contact.PrintEveryNum(contact_list, 1000);

            totalTime.Stop();
            long totalMs = totalTime.ElapsedMilliseconds;
            Console.WriteLine($"\nTime elapsed: {totalMs} milliseconds.");
        }
    }
}
