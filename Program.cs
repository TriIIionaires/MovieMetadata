using Microsoft.VisualBasic.FileIO;
using System.Text.Json;
using Newtonsoft.Json;
using MovieMetadata.Models;

namespace MovieMetadata
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = @"..\..\..\CSV Files\movies_metadata.csv";

            using (TextFieldParser csvParser = new TextFieldParser(path))
            {
                csvParser.CommentTokens = new string[] { "#" };
                csvParser.SetDelimiters(new string[] { "," });
                csvParser.HasFieldsEnclosedInQuotes = true;

                csvParser.ReadLine();

                while (!csvParser.EndOfData)
                {
                    
                    string[] fields = csvParser.ReadFields();
                    
                    if (fields[0].Equals("False") && fields[7].Equals("en") && fields.Length == 24) //Omits adult and non-english movies
                    {
                        MetadataModel movie = new MetadataModel()
                        {
                            Movie_ID = Int32.Parse(fields[5]),
                            Homepage = fields[4],
                            PosterPath = fields[11]
                        };

                        if (!fields[6].Equals(""))
                            movie.IMDB_ID = Int32.Parse(fields[6].Substring(2));

                        if (!fields[8].Equals(""))
                            movie.Title = fields[8];

                        if (!fields[9].Equals(""))
                            movie.Description = fields[9];

                        DateTime date;
                            bool isDate = DateTime.TryParse(fields[14], out date);
                            if (isDate) movie.ReleaseDate = date;

                        Double runtime;
                            bool isRunTime = Double.TryParse(fields[16], out runtime);
                            if (isRunTime) movie.RunTime = (int) runtime;


                        List<GenreModel> genres = new List<GenreModel>();
                        dynamic json = JsonConvert.DeserializeObject(fields[3]);

                        if (json != null)
                        {
                            foreach (var g in json)
                            {
                                GenreModel genre = new GenreModel()
                                {
                                    Genre_ID = g.id,
                                    Genre = g.name
                                };

                                genres.Add(genre);

                            }
                        }

                        if (genres.Count > 0) movie.Genres = genres;

                        Console.WriteLine(movie);
                    }
                }
            }
        }
    }
}
