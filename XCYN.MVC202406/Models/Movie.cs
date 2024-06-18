using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XCYN.MVC202406.Models
{
    public class Movie
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string Genre { get; set; }

        public decimal Price { get; set; }
    }
}