from Contact import Contact
import time

def main():
    # record time at start
    # GPT: Is Python's time.time() in seconds or in milliseconds?
    start = time.time()
    # csv file to import contacts
    filePath1 = 'us-contacts.csv'
    filePath2 = 'export.csv'
    contact_list = []

    contact_list = Contact.ReadContactsFromFile(filePath1)
    contact_list = Contact.SortByLast(contact_list)

    print("\nContacts imported from us-contacts.csv, sorted by last name, printing every 50 contacts...\n")
    Contact.PrintEveryNum(contact_list, 50)

    contact_list = Contact.ReadContactsFromFile(filePath2)
    contact_list = Contact.SortByLast(contact_list)

    print("\nContacts imported from export.csv, sorted by last name, printing every 1000 contacts...\n")
    Contact.PrintEveryNum(contact_list, 1000)

    end = time.time()
    exec_time = (end - start) * 1000
    print("\nTime elapsed:", int(exec_time), "milliseconds")  
    
if __name__ == "__main__":
    main()
