using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xamarin.Essentials;

namespace Homework04.Models
{
    public class User
    {
        public static int GlobalUserID = -1;

        public int ID { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public User() { }
        public User(string email, string username)
        {
            ID = Interlocked.Increment(ref GlobalUserID);
            Email = email;
            Username = username;
        }
    }
}
