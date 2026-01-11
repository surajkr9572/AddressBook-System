using AddressBook.Entity;
using AddressBook.Service;
using NUnit.Framework;

namespace AddressBookTest
{
    [TestFixture]
    public class Class1
    {
        private AddressBookService addressBookService;

        [SetUp]
        public void Init()
        {
            addressBookService = new AddressBookService();

            addressBookService.CreateAddressBook("Book1");
            addressBookService.CreateAddressBook("Book2");
            addressBookService.CreateAddressBook("Book3");
            
        }

        [Test]
        public void GivenExistingAddressBook_WhenCallCreateAddressBook_ThenReturnFalse()
        {
            // Arrange
            var existingBookName = "Book1";

            var expected = false;

            //Act 
            var actual = addressBookService.CreateAddressBook(existingBookName);

            // Assert0
            Assert.That(actual,Is.EqualTo(expected));
        }
        [Test]
        
        public void GivenExistingAddressBook_WhenCallCreateAddressBook_ThenReturnTrue()
        {
            //Arrange
            var existingBookName = "Book5";
            var expected = true;

            //Act
            var actual= addressBookService.CreateAddressBook(existingBookName);

            //Assert
            Assert.That(actual,Is.EqualTo(expected));
        }
        [Test]

        public void GivenExistingAddressBook_WhenCallSelectAddressBook_ThenReturnFalse()
        {
            //Arrange
            var existingBookName = "Book4";
            var expected = false;

            //Act
            var actual=addressBookService.SelectAddressBook(existingBookName);

            //Assert
            Assert.That(actual,Is.EqualTo(expected));
        }

        [Test]

        public void GivenExistingAddressBook_WhenCallSelectAddressBook_ThenReturnTrue()
        {
            //Arrange
            var existingBookName = "Book2";
            var expected = true;

            //Act
            var actual = addressBookService.SelectAddressBook(existingBookName);

            //Assert
            Assert.That( actual,Is.EqualTo(expected));
        }

        [Test]

        public void GivenValidContactWithAllFeild_WhenAddContact_ThenReturnTrue()
        {
            //Arrange
            Contacts contact = new Contacts("Suraj")
            {
                LastName = "Kumar",
                Address = "Belsand",
                City = "Gopalganj",
                State="Bihar",
                PhoneNumber="9572790286",
                Email="surajsingh43007@gmail.com",
                ZipCode="841413"
            };
            var expected = true;

            //Act
            var actual=addressBookService.AddContact(contact);

            //Assert
            Assert.That(actual,Is.EqualTo(expected));
        }

        [Test]
        public void GivenDuplicateFirstName_WhenAddContact_ThenReturnFalse()
        {

            //Arrange
            Contacts contact = new Contacts("Suraj1")
            {
                LastName = "Kumar",
                Address = "Belsand",
                City = "Gopalganj",
                State = "Bihar",
                PhoneNumber = "9572790299",
                Email = "suraj1singh43007@gmail.com",
                ZipCode = "841413"
            };
            Contacts contact1 = new Contacts("Sachin")
            {
                LastName = "Kumar",
                Address = "Belsand",
                City = "Gopalganj",
                State = "Bihar",
                PhoneNumber = "9572790287",
                Email = "sachinsingh43007@gmail.com",
                ZipCode = "841413"
            };
            Contacts contact2 = new Contacts("Suraj1")
            {
                LastName = "Kumar",
                Address = "Belsand",
                City = "Gopalganj",
                State = "Bihar",
                PhoneNumber = "9572790280",
                Email = "sachin1singh43007@gmail.com",
                ZipCode = "841413"
            };
            var expected = false;

            //Act
            addressBookService.AddContact(contact);
            addressBookService.AddContact(contact1);
            var actual2= addressBookService.AddContact(contact2);

            //Assert
            Assert.That(actual2, Is.EqualTo(expected));
        }
        [Test]
       public void GivenAddressSelectedAndContactsAdded_WhenGetAllContacts_ThenReturnAllContains()
        {
            //Arrange
            addressBookService.SelectAddressBook("Book2");

            Contacts contact = new Contacts("Suraj1")
            {
                LastName = "Kumar",
                Address = "Belsand",
                City = "Gopalganj",
                State = "Bihar",
                PhoneNumber = "9572790299",
                Email = "suraj1singh43007@gmail.com",
                ZipCode = "841413"
            };
            Contacts contact1 = new Contacts("Sachin")
            {
                LastName = "Kumar",
                Address = "Belsand",
                City = "Gopalganj",
                State = "Bihar",
                PhoneNumber = "9572790287",
                Email = "sachinsingh43007@gmail.com",
                ZipCode = "841413"
            };
            
            addressBookService.AddContact( contact);
            addressBookService.AddContact( contact1);
            var expected = 2;
            

            //Act
            var contacts=addressBookService.GetAllContacts();

            //Assert
            Assert.That(contacts,Is.Not.Null);
            Assert.That(contacts.Count, Is.EqualTo(expected));
        }
        [Test]
        public void GivenNoAddressBookSelected_WhenGetAllContacts_ThenReturnEmptyList()
        {
            // Act
            var contacts = addressBookService.GetAllContacts();

         
            // Assert
            Assert.That(contacts, Is.Not.Null);
            Assert.That(contacts, Is.Empty);
        }

        [Test]
        public void GivenAddressSelectedButNoContactAdded_WhenGetAllContacts_ThenReturnEmptyList()
        {
            //Arrange
            addressBookService.SelectAddressBook("Book2");


            //Act
            var contacts = addressBookService.GetAllContacts();
            //Assert
            Assert.That(contacts, Is.Not.Null);
            Assert.That(contacts, Is.Empty);
        }

        [Test]
        
        
        public void GivenMultipleAddressBooks_WhenGetAllAddressBookNames_ThenReturnAllNames()
        {
            var expected = 3;
            //Act
            var contacts = addressBookService.GetAllAddressBookNames();
            //Assert
            Assert.That(contacts, Is.Not.Null);
            Assert.That(contacts.Count, Is.EqualTo(expected));
        }

        [Test]
        public void GivenMultipleAddressBooks_WhenSearchByCity_ThenReturnMatchingContacts()
        {
            // Arrange
            // Select first address book
            addressBookService.SelectAddressBook("Book2");

            Contacts contact1 = new Contacts("Suraj1")
            {
                LastName = "Kumar",
                Address = "Belsand",
                City = "Gopalganj",
                State = "Bihar",
                PhoneNumber = "9572790299",
                Email = "suraj1singh43007@gmail.com",
                ZipCode = "841413"
            };

            Contacts contact2 = new Contacts("Sachin")
            {
                LastName = "Kumar",
                Address = "Belsand",
                City = "Lucknow",
                State = "Bihar",
                PhoneNumber = "9572790287",
                Email = "sachinsingh43007@gmail.com",
                ZipCode = "841413"
            };

            // Act
            var added1 = addressBookService.AddContact(contact1);
            var added2 = addressBookService.AddContact(contact2);

            // Assert
            Assert.That(added1, Is.True);
            Assert.That(added2, Is.True);

            // Select second address book
            addressBookService.SelectAddressBook("Book3");


            //Arrange
            Contacts contact3 = new Contacts("Suraj1") 
            {
                LastName = "Kumar",
                Address = "Belsand",
                City = "Gopalganj",
                State = "Bihar",
                PhoneNumber = "9572790299",
                Email = "suraj1singh43007@gmail.com",
                ZipCode = "841413"
            };

            Contacts contact4 = new Contacts("Sachin")
            {
                LastName = "Kumar",
                Address = "Belsand",
                City = "Lucknow",
                State = "Bihar",
                PhoneNumber = "9572790287",
                Email = "sachinsingh43007@gmail.com",
                ZipCode = "841413"
            };
            //Act
            var added3 = addressBookService.AddContact(contact3);
            var added4 = addressBookService.AddContact(contact4);
            //Assert
            Assert.That(added3, Is.True);
            Assert.That(added4, Is.True);

            var expected = 2;
            //Act
            var actual = addressBookService.SearchCity("Gopalganj");

            //Assert
            Assert.That(actual.Count, Is.EqualTo(expected));
        }
        [Test]
        public void GivenMultipleAddressBooks_WhenSearchByState_ThenReturnMatchingContacts()
        {
            // Arrange
            // Select first address book
            addressBookService.SelectAddressBook("Book2");

            Contacts contact1 = new Contacts("Suraj1")
            {
                LastName = "Kumar",
                Address = "Belsand",
                City = "Gopalganj",
                State = "Bihar",
                PhoneNumber = "9572790299",
                Email = "suraj1singh43007@gmail.com",
                ZipCode = "841413"
            };

            Contacts contact2 = new Contacts("Sachin")
            {
                LastName = "Kumar",
                Address = "Belsand",
                City = "Lucknow",
                State = "Up",
                PhoneNumber = "9572790287",
                Email = "sachinsingh43007@gmail.com",
                ZipCode = "841413"
            };

            // Act
            var added1 = addressBookService.AddContact(contact1);
            var added2 = addressBookService.AddContact(contact2);

            // Assert
            Assert.That(added1, Is.True);
            Assert.That(added2, Is.True);

            // Select second address book
            addressBookService.SelectAddressBook("Book3");


            //Arrange
            Contacts contact3 = new Contacts("Suraj1")
            {
                LastName = "Kumar",
                Address = "Belsand",
                City = "Gopalganj",
                State = "Bihar",
                PhoneNumber = "9572790299",
                Email = "suraj1singh43007@gmail.com",
                ZipCode = "841413"
            };

            Contacts contact4 = new Contacts("Sachin")
            {
                LastName = "Kumar",
                Address = "Belsand",
                City = "Lucknow",
                State = "Up",
                PhoneNumber = "9572790287",
                Email = "sachinsingh43007@gmail.com",
                ZipCode = "841413"
            };
            //Act
            var added3 = addressBookService.AddContact(contact3);
            var added4 = addressBookService.AddContact(contact4);
            //Assert
            Assert.That(added3, Is.True);
            Assert.That(added4, Is.True);

            var expected = 2;
            //Act
            var actual = addressBookService.SearchState("Bihar");

            //Assert
            Assert.That(actual.Count, Is.EqualTo(expected));
        }
       
        [Test]
        public void GivenMultipleAddressBook_WhenCountStateInMultipleCity_ThenReturnStateCount()
        {
            //Arrange
            addressBookService.SelectAddressBook("Book2");

            Contacts contact1 = new Contacts("Suraj1")
            {
                LastName = "Kumar",
                Address = "Belsand",
                City = "Gopalganj",
                State = "Bihar",
                PhoneNumber = "9572790299",
                Email = "suraj1singh43007@gmail.com",
                ZipCode = "841413"
            };

            Contacts contact2 = new Contacts("Sachin")
            {
                LastName = "Kumar",
                Address = "Belsand",
                City = "Lucknow",
                State = "Up",
                PhoneNumber = "9572790287",
                Email = "sachinsingh43007@gmail.com",
                ZipCode = "841413"
            };

            // Act
            var added1 = addressBookService.AddContact(contact1);
            var added2 = addressBookService.AddContact(contact2);

            //// Assert
            //Assert.That(added1, Is.True);
            //Assert.That(added2, Is.True);

            // Select second address book
            addressBookService.SelectAddressBook("Book3");


            //Arrange
            Contacts contact3 = new Contacts("Suraj1")
            {
                LastName = "Kumar",
                Address = "Belsand",
                City = "Gopalganj",
                State = "Bihar",
                PhoneNumber = "9572790299",
                Email = "suraj1singh43007@gmail.com",
                ZipCode = "841413"
            };

            Contacts contact4 = new Contacts("Sachin")
            {
                LastName = "Kumar",
                Address = "Belsand",
                City = "Lucknow",
                State = "Up",
                PhoneNumber = "9572790287",
                Email = "sachinsingh43007@gmail.com",
                ZipCode = "841413"
            };
            //Act
            var added3 = addressBookService.AddContact(contact3);
            var added4 = addressBookService.AddContact(contact4);

            var expected = 2;
            //Act
            var actual = addressBookService.CountState("Bihar");
            //Assert
            Assert.That(actual, Is.EqualTo(expected));

        }
        [Test]
        public void GivenMultipleAddressBook_WhenCountCityInMultipleCity_ThenReturnCityCount()
        {
            //Arrange
            addressBookService.SelectAddressBook("Book2");

            Contacts contact1 = new Contacts("Suraj1")
            {
                LastName = "Kumar",
                Address = "Belsand",
                City = "Gopalganj",
                State = "Bihar",
                PhoneNumber = "9572790299",
                Email = "suraj1singh43007@gmail.com",
                ZipCode = "841413"
            };

            Contacts contact2 = new Contacts("Sachin")
            {
                LastName = "Kumar",
                Address = "Belsand",
                City = "Lucknow",
                State = "Up",
                PhoneNumber = "9572790287",
                Email = "sachinsingh43007@gmail.com",
                ZipCode = "841413"
            };

            // Act
            var added1 = addressBookService.AddContact(contact1);
            var added2 = addressBookService.AddContact(contact2);


            // Select second address book
            addressBookService.SelectAddressBook("Book3");


            //Arrange
            Contacts contact3 = new Contacts("Suraj1")
            {
                LastName = "Kumar",
                Address = "Belsand",
                City = "Gopalganj",
                State = "Bihar",
                PhoneNumber = "9572790299",
                Email = "suraj1singh43007@gmail.com",
                ZipCode = "841413"
            };

            Contacts contact4 = new Contacts("Sachin")
            {
                LastName = "Kumar",
                Address = "Belsand",
                City = "Lucknow",
                State = "Up",
                PhoneNumber = "9572790287",
                Email = "sachinsingh43007@gmail.com",
                ZipCode = "841413"
            };
            //Act
            var added3 = addressBookService.AddContact(contact3);
            var added4 = addressBookService.AddContact(contact4);

            var expected = 2;
            //Act
            var actual = addressBookService.CountCity("Gopalganj");
            //Assert
            Assert.That(actual, Is.EqualTo(expected));
        }
        [Test]
        public void GivenSelectedAddressBookWithExistingContact_WhenDeletePerson_ThenContactIsRemoved()
        {
            addressBookService.SelectAddressBook("Book2");

            Contacts contact1 = new Contacts("Suraj")
            {
                LastName = "Kumar",
                Address = "Belsand",
                City = "Gopalganj",
                State = "Bihar",
                PhoneNumber = "9572790299",
                Email = "suraj1singh43007@gmail.com",
                ZipCode = "841413"
            };

            Contacts contact2 = new Contacts("Sachin")
            {
                LastName = "Kumar",
                Address = "Belsand",
                City = "Lucknow",
                State = "Up",
                PhoneNumber = "9572790287",
                Email = "sachinsingh43007@gmail.com",
                ZipCode = "841413"
            };
            Contacts contact3 = new Contacts("Suraj1")
            {
                LastName = "Kumar",
                Address = "Belsand",
                City = "Gopalganj",
                State = "Bihar",
                PhoneNumber = "9572790299",
                Email = "suraj1singh43007@gmail.com",
                ZipCode = "841413"
            };

            Contacts contact4 = new Contacts("Sachin1")
            {
                LastName = "Kumar",
                Address = "Belsand",
                City = "Lucknow",
                State = "Up",
                PhoneNumber = "9572790287",
                Email = "sachinsingh43007@gmail.com",
                ZipCode = "841413"
            };
            addressBookService.AddContact(contact1);
            addressBookService.AddContact(contact2);
            addressBookService.AddContact(contact3);
            addressBookService.AddContact(contact4);

            var expected = 3;
            //Act
            addressBookService.DeletePerson("Suraj1");
            var actual = addressBookService.GetAllContacts();
            //Assert
            Assert.That(actual.Count,Is.EqualTo(expected));
        }
        [Test]
        public void GivenSelectedAddressBookWithNonExistingContact_WhenDelectContact_ThenListRemainSame()
        {
            addressBookService.SelectAddressBook("Book2");

            Contacts contact1 = new Contacts("Suraj")
            {
                LastName = "Kumar",
                Address = "Belsand",
                City = "Gopalganj",
                State = "Bihar",
                PhoneNumber = "9572790299",
                Email = "suraj1singh43007@gmail.com",
                ZipCode = "841413"
            };

            Contacts contact2 = new Contacts("Sachin")
            {
                LastName = "Kumar",
                Address = "Belsand",
                City = "Lucknow",
                State = "Up",
                PhoneNumber = "9572790287",
                Email = "sachinsingh43007@gmail.com",
                ZipCode = "841413"
            };
            Contacts contact3 = new Contacts("Suraj1")
            {
                LastName = "Kumar",
                Address = "Belsand",
                City = "Gopalganj",
                State = "Bihar",
                PhoneNumber = "9572790299",
                Email = "suraj1singh43007@gmail.com",
                ZipCode = "841413"
            };

            Contacts contact4 = new Contacts("Sachin1")
            {
                LastName = "Kumar",
                Address = "Belsand",
                City = "Lucknow",
                State = "Up",
                PhoneNumber = "9572790287",
                Email = "sachinsingh43007@gmail.com",
                ZipCode = "841413"
            };
            addressBookService.AddContact(contact1);
            addressBookService.AddContact(contact2);
            addressBookService.AddContact(contact3);
            addressBookService.AddContact(contact4);

            addressBookService.DeletePerson("Aryan");

            var expected = 4;
            
            //Act
            var actual=addressBookService.GetAllContacts();

            //Assert
            Assert.That(actual.Count, Is.EqualTo(expected));
        }
    }
}

