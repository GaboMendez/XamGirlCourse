using System;
using System.Collections.Generic;
using System.Text;

namespace Homework05.Models
{
    public class Post
    {
        public string Image { get; set; }
        public string Title { get; set; }

        public Post(string title, string image)
        {
            Title = title;
            Image = image;
        }
    }
}
