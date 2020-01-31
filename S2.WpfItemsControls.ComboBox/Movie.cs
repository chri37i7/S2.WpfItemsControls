using System;
using System.Collections.Generic;
using System.Text;

namespace S2.WpfItemsControls.ComboBox
{
    class Movie
    {
        // Constructor
        public Movie(string title, string genre, string leadActor, int playtime, DateTime releaseDate)
        {
            Title = title;
            Genre = genre;
            LeadActor = leadActor;
            Playtime = playtime;
            ReleaseDate = releaseDate;
        }

        // Properties
        public string Title { get; set; }
        public string Genre { get; set; }
        public string LeadActor { get; set; }
        public int Playtime { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}