using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Homework05.Models
{
    public class AnimeList
    {
        public ObservableCollection<Top> top { get; set; }
        public ObservableCollection<Top> results { get; set; }
    }

    public class Top
    {
        public int mal_id { get; set; }
        public int rank { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public string image_url { get; set; }
        public string type { get; set; }
        public int? episodes { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public int members { get; set; }
        public double score { get; set; }
    }
}
