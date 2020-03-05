using MonkeyCache.FileStore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Homework03.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public User() { }
        public User(string email, string username)
        {
            var UserID = Barrel.Current.Get<int>(key: "LastUserID");
            ID = Interlocked.Increment(ref UserID);
            Email = email;
            Username = username;
        }
    }
}
