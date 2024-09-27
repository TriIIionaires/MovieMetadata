using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieMetadata.Models
{
    public class GenreModel
    {
        public int Movie_ID { get; set; }
        public int Genre_ID { get; set; }
        public string Genre { get; set; }

        public override string ToString()
        {
            return $"{Genre}";
        }
    }
}
