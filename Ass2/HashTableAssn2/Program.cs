using System;

namespace Assn2
{
    class Program
    {
        public static void Main(string[] args)
        {
            List<Contact> contact_list = new List<Contact>();

            string contactFilePath = "newexport.csv";
            contact_list = Contact.ReadContactsFromFile(contactFilePath);
            CustomHashTable<string, Contact> h1 = new CustomHashTable<string, Contact>();

            // Insert contacts into hash table h1
            for (int i = 0; i < contact_list.Count; i++)
            {
                h1.Insert(contact_list[i].EmployeeID, contact_list[i]);
            }

            // Print all items in hash table
            h1.PrintEntries();
            Console.WriteLine();

            // remove id's 1 - 10
            for (int i = 1; i <= 10; i++)
            {
                h1.Remove(i.ToString());

                // Try to call the "Get" method
                try
                {
                    Console.WriteLine($"Key found: {h1.Get(i.ToString())}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unable to find key number {i}: {ex.Message}");
                }
            }
        }
    }
}