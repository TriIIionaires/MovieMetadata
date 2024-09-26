using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieMetadata.Models
{
    public class MetadataModel
    {
        
        public enum Ratings
        {
            G,
            PG,
            PG13,
            R
        }

        public int ID { get; set; }
        public int Movie_ID { get; set; }
        public int IMDB_ID { get; set; }
        public string Title { get; set; } = "Unknown Title";
        public List<GenreModel> Genres { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int RunTime { get; set; }
        public Ratings Rating { get; set; }
        public string Description { get; set; } = "No description provided.";
        public string Homepage { get; set; }
        public string PosterPath { get; set; }

        public override string ToString()
        {
            string output = $"{Title}\n";
            
            if (Genres != null)
            {
                foreach (GenreModel genre in Genres)
                {
                    output += $"{genre.Genre}, ";
                }

                output = output.Substring(0, output.Length - 2); //Omits last comma and space

            } else
            {
                output += "Genre Unavailable";
            }
            
            output += $"\n{ReleaseDate.ToShortDateString()} \u00B7 {Rating} \u00B7 {RunTime / 60}h {RunTime % 60}m\n{Description}\nID: {ID} | Movie ID: {Movie_ID} | IMDB ID: {IMDB_ID}\n";

            return output;
        }
    }
}
