using AddressBook.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AddressBook.Entity
{
    public class Contacts:IComparable<Contacts>
    {
        private string firstName;
        private string lastName;
        private string address;
        private string city;
        private string state;
        private string zipCode;
        private string phoneNumber;
        private string email;
        public string FirstName
        {
            get { return firstName; }
            private set { firstName = value; }
        }

        public Contacts(string firstName)
        {
            this.firstName = firstName;
        }
        public string LastName
        {
            get { return lastName; }
            set { lastName= value; }
        }
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        public string City
        {
            get { return city; }
            set { city=value; }
        }
        public string State
        {
            get { return state; }
            set { state=value; }
        }
       
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set
            {
                string pattern = @"^[6-9]\d{9}$";
                if (Regex.IsMatch(value, pattern))
                {
                    phoneNumber = value;
                }
                else
                {
                    throw new PhoneNumberException("Invalid phone number");
                }
            }
        }

        public string Email
        {
            get { return email; }
            set
            {
                string pattern = @"^[\w._%+-]+@[\w.-]+\.[a-zA-Z]{2,}$";
                if (Regex.IsMatch(value, pattern))
                {
                    email = value;
                }
                else
                {
                    throw new EmailException("Invalid email format");
                }
            }
        }
        public string ZipCode
        {
            get { return zipCode; }
            set
            {
                string pattern = @"^[0-9]{6}$";
                if(Regex.IsMatch(value, pattern))
                {
                    zipCode=value;
                }
                else
                {
                    throw new ZipCodeException("Invalid ZipCode");
                }
            }
        }
        public override bool Equals(object obj)
        {
            if(obj == null) return false;
            if(ReferenceEquals(this,obj) ) return true;
            if (obj is not Contacts other) return false;
            
            return this.FirstName == other.FirstName;

        }

        //Override GetAllContacts method
        public override string ToString()
        {
            return $"{firstName,-12}{lastName,-12}{address,-12}{city,-10}{state,-8}{zipCode,-8}{phoneNumber,-12}{Email}";
        }

        //Sort using Comparater

        public int CompareTo(Contacts other)
        {
            return this.FirstName.CompareTo(other.FirstName);
        }
    }
}
