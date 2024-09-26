using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieMetadata.Models;

namespace MovieData.Data
{
    public class MovieData
    {
        private readonly ISqlDataAccess _db = new SqlDataAccess();

        public List<MetadataModel> ReadAllMovies()
        {
            //string sql = "SELECT * FROM movie";
            string sql = "SELECT movie.id, title, description, runtime, releasedate, ratings.rating FROM theater.movie INNER JOIN theater.ratings ON movie.rating=ratings.movierating";

            IEnumerable<MetadataModel> result = _db.LoadData<MetadataModel, dynamic>(sql, new { });
            List<MetadataModel> movies = result.ToList();

            if (movies.Count > 0) return movies;
            return null;
        }

        public MetadataModel ReadMovieByID(int id)
        {
            string sql = "SELECT * FROM movie WHERE movie.ID = @id";

            IEnumerable<MetadataModel> result = _db.LoadData<MetadataModel, dynamic>(sql, new { id });
            List<MetadataModel> movies = result.ToList();

            if (movies.Count > 0) return movies[0];
            return null;
        }

        public void CreateMovie(MetadataModel movie)
        {
            string sql = "INSERT INTO movie (Title, Description, RunTime, Rating, ReleaseDate) VALUES (@Title, @Description, @RunTime, @Rating, @ReleaseDate)";

            _db.SaveData(sql, movie);
        }

        public void UpdateMovie(MetadataModel movie)
        {
            string sql = "UPDATE movie SET Title = @Title, Description = @Description, RunTime = @RunTime, Rating = @Rating, ReleaseDate = @ReleaseDate WHERE movie.ID = @ID";

            _db.SaveData(sql, movie);
        }

        public void DeleteMovie(int id)
        {
            string sql = "DELETE FROM movie WHERE movie.ID = @id";

            _db.SaveData(sql, new { id });
        }

        public List<GenreModel> GetMovieGenres(int id)
        {
            string sql = "SELECT genres.genre FROM genres INNER JOIN asgngenre ON genres.genreID = asgngenre.genreID WHERE asgngenre.movieID = @id";

            List<GenreModel> result = _db.LoadData<GenreModel, dynamic>(sql, new { id });

            if (result.Count > 0) return result;
            return null;
        }

        /*public List<string> GetMovieGenres(int id)
        {
            string sql = "SELECT genres.genre FROM genres INNER JOIN asgngenre ON genres.genreID = asgngenre.genreID WHERE asgngenre.movieID = @id";

            List<string> result = _db.LoadData<string, dynamic>(sql, new { id });
            
            if (result.Count > 0) return result;
            return null;
        }*/
    }
}
