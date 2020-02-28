using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Homework04.Models
{
    public class Contact
    {
        public static int GlobalContactID = -1;
        public int ID { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string Phone { get; set; }
        public string TypePhone { get; set; }
        public string Email { get; set; }
        public string TypeEmail { get; set; }

        public string FullName { get { return $" {this.FirstName} {this.LastName}"; } }

        public Contact()
        {
            ID = Interlocked.Increment(ref GlobalContactID);
        }

        public Contact(string firstName, string lastName, string phone,string category, string image)
        {
            ID = Interlocked.Increment(ref GlobalContactID);
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Category = category;
            Image = image;
        }

        public Contact(string category, string image, string firstName, string lastName, string company, string phone, string typePhone, string email, string typeEmail)
        {
            ID = Interlocked.Increment(ref GlobalContactID);
            Category = category;
            Image = image;
            FirstName = firstName;
            LastName = lastName;
            Company = company;
            Phone = phone;
            TypePhone = typePhone;
            Email = email;
            TypeEmail = typeEmail;
        }
    }
}
