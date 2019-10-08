using System;
using System.Collections.Generic;
using System.Text;

namespace IRunes.App.Models
{
    public class Album
    {
        public Album()
        {
            Tracks = new List<Track>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Cover { get; set; }

        public decimal Price { get; set; }

        public List<Track> Tracks { get; set; }
    }

}
