using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using MovieMetadata.Models;
using MovieMetadata.Data;
using System.Runtime.CompilerServices;
using System.Net.Http.Json;

namespace MovieMetadata
{
    internal class Program
    {
        static void Main(string[] args)
        {

            MovieData _db = new MovieData();
            
            string mmd_path = @"C:\Users\Jadon\Downloads\MovieCSV\movies_metadata.csv"; // Change path to match your own

            /*using (TextFieldParser csvParser = new TextFieldParser(mmd_path)) // DO NOT RUN THE PROGRAM
            {
                csvParser.CommentTokens = new string[] { "#" };
                csvParser.SetDelimiters(new string[] { "," });
                csvParser.HasFieldsEnclosedInQuotes = true;

                csvParser.ReadLine();

                while (!csvParser.EndOfData)
                {
                    
                    string[] fields = csvParser.ReadFields();
                    
                    if (fields[0].Equals("False") && fields[7].Equals("en") && fields.Length == 24) // Omits adult and non-english movies
                    {
                        MetadataModel movie = new MetadataModel()
                        {
                            Movie_ID = Int32.Parse(fields[5]),
                            IMDB_ID = fields[6],
                            Tagline = fields[19],
                            Homepage = fields[4],
                            PosterURL = fields[11]
                        };

                        string title = "Unknown Title";
                            if (!fields[8].Equals("")) title = fields[8];
                            movie.Title = title;

                        string description = "No description provided";
                            if (!fields[9].Equals("")) description = fields[9];
                            movie.Description = description;

                        DateTime date;
                            bool isDate = DateTime.TryParse(fields[14], out date);
                            if (isDate) movie.Release_Date = date;

                        Double runtime;
                            bool isRunTime = Double.TryParse(fields[16], out runtime);
                            if (isRunTime) movie.RunTime = (int) runtime;

                        Double rating;
                            bool isRating = Double.TryParse(fields[22], out rating);
                            if (isRating) movie.Rating = rating;

                        int votes;
                            bool isVotes = Int32.TryParse(fields[23], out votes);
                            if (isVotes) movie.Votes = votes;

                        List<GenreModel> genres = new List<GenreModel>();
                        dynamic json = JsonConvert.DeserializeObject(fields[3]);

                        if (json != null)
                        {
                            foreach (var g in json)
                            {
                                GenreModel genre = new GenreModel()
                                {
                                    Movie_ID = Int32.Parse(fields[5]),
                                    Genre_ID = g.id,
                                    Genre = g.name
                                };
                                
                                genres.Add(genre);

                            }
                        }

                        if (genres.Count > 0)
                        {
                            movie.Genres = genres;
                            _db.CreateAssignGenre(genres);
                        }

                        _db.CreateMovie(movie);

                    }
                }
            }*/
            
            
            /*HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://www.omdbapi.com");

            for (int i = 0; i <= 32262; i++)
            {
                MetadataModel movie = _db.ReadMovieByID(i);
                if (movie != null)
                {
                    if (!movie.IMDB_ID.Equals(""))
                    {
                        HttpResponseMessage response = client.GetAsync($"?i={movie.IMDB_ID}&apikey=").Result;
                        if (response.IsSuccessStatusCode)
                        {
                            string data = response.Content.ReadAsStringAsync().Result;
                            dynamic json = JsonConvert.DeserializeObject(data);
                            string rating = json.Rated.ToString();

                            if (rating.Equals("R"))
                            {
                                movie.MPAA_RatingID = MetadataModel.Ratings.R;
                            }
                            else if (rating.Equals("PG-13"))
                            {
                                movie.MPAA_RatingID = MetadataModel.Ratings.PG13;
                            }
                            else if (rating.Equals("PG"))
                            {
                                movie.MPAA_RatingID = MetadataModel.Ratings.PG;
                            }
                            else
                            {
                                movie.MPAA_RatingID = MetadataModel.Ratings.G;
                            }

                            _db.UpdateMovie(movie);
                        }
                    }
                }
            }*/

            List<MetadataModel> movies = _db.ReadAllMovies();
            
            if (movies != null)
            {
                foreach (MetadataModel movie in movies)
                {
                    movie.Genres = _db.GetMovieGenres(movie.Movie_ID);
                    Console.WriteLine(movie);
                }
            }
        }
    }
}
