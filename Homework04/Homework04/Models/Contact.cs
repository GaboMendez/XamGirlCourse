using System;
using System.Collections.Generic;
using System.Text;

namespace Homework04.Models
{
    public class Contact
    {
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

        public Contact() { }

        public Contact(int id, string category, string image, string firstName, string lastName, string company, string phone, string typePhone, string email, string typeEmail)
        {
            ID = id;
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
