using System;
using System.Collections.Generic;
using System.Text;

namespace Homework05.Models
{
    public class Anime
    {
        public int mal_id { get; set; }
        public string url { get; set; }
        public string image_url { get; set; }
        public string trailer_url { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public int? episodes { get; set; }
        public string status { get; set; }
        public string rating { get; set; }
        public double score { get; set; }
        public string synopsis { get; set; }
        public string background { get; set; }
        public string premiered { get; set; }
        public Aired aired { get; set; }
        public List<Genre> genres { get; set; }
        public List<string> opening_themes { get; set; }
        public List<string> ending_themes { get; set; }
    }

    public class Aired
    {
        public string String { get; set; }
    }

    public class Genre
    {
        public int mal_id { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

}
