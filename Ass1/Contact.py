# Contact class

class Contact:
    def __init__(self, firstName, lastName, street, city, state, zip, phone, email):
        # New Contact is initialized
        self.firstName = firstName
        self.lastName = lastName
        self.street = street
        self.city = city
        self.state = state
        self.zip = zip
        self.phone = phone
        self.email = email
    
    @classmethod
    def ReadContactsFromFile(cls, filePath):
        contact_list = []

        try:
            with open(filePath, 'r') as file: # Will automatically close when finished
                for line in file:
                    param_list = line.split(',')
                    contact = Contact(param_list[0],
                                      param_list[1],
                                      param_list[2],
                                      param_list[3],
                                      param_list[4],
                                      param_list[5],
                                      param_list[6],
                                      param_list[7])
                    contact_list.append(contact)

            return contact_list
        
        except:
            print("Unable to read from file.")

    @classmethod
    def PrintContact(cls, contact):
        print(contact.firstName + ", " +
              contact.lastName + ", " +
              contact.street + ", " +
              contact.city + ", " +
              contact.state + ", " +
              contact.zip + ", " +
              contact.phone + ", " +
              contact.email,
              end = "")

#GPT: how to create a static method in Python?
    @classmethod
    def PrintAllContacts(cls, contacts):
        for contact in contacts:
            cls.PrintContact(contact)

    @classmethod
    def SortByLast(cls, contacts):
        sorted_contacts = sorted(contacts, key=lambda contact: contact.lastName)
        return sorted_contacts

    @classmethod
    def PrintEveryNum(cls, contacts, skip):
        for i in range(skip - 1, len(contacts), skip):
            cls.PrintContact(contacts[i])

    