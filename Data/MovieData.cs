using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieMetadata.Models;

namespace MovieMetadata.Data
{
    public class MovieData
    {
        private readonly ISqlDataAccess _db = new SqlDataAccess();

        public List<MetadataModel> ReadAllMovies()
        {
            string sql = "SELECT * FROM mmd_movie";

            IEnumerable<MetadataModel> result = _db.LoadData<MetadataModel, dynamic>(sql, new { });
            List<MetadataModel> movies = result.ToList();

            if (movies.Count > 0) return movies;
            return null;
        }

        public MetadataModel ReadMovieByID(int id)
        {
            string sql = "SELECT * FROM mmd_movie WHERE mmd_movie.ID = @id";

            IEnumerable<MetadataModel> result = _db.LoadData<MetadataModel, dynamic>(sql, new { id });
            List<MetadataModel> movies = result.ToList();

            if (movies.Count > 0) return movies[0];
            return null;
        }

        public void CreateMovie(MetadataModel movie)
        {
            string sql = "INSERT INTO mmd_movie (Movie_ID, IMDB_ID, Title, Release_Date, RunTime, Rating, Votes, Tagline, Description, Homepage, PosterURL) VALUES (@Movie_ID, @IMDB_ID, @Title, @Release_Date, @RunTime, @Rating, @Votes, @Tagline, @Description, @Homepage, @PosterURL)";

            _db.SaveData(sql, movie);
        }

        public void UpdateMovie(MetadataModel movie)
        {
            string sql = "UPDATE mmd_movie SET Movie_ID = @Movie_ID, IMDB_ID = @IMDB_ID, Title = @Title, RunTime = @RunTime, Rating = @Rating, Votes = @Votes, Tagline = @Tagline, Description = @Description, Homepage = @Homepage, PosterURL = @PosterURL WHERE mmd_movie.ID = @ID";

            _db.SaveData(sql, movie);
        }

        public void DeleteMovie(int id)
        {
            string sql = "DELETE FROM mmd_movie WHERE mmd_movie.ID = @id";

            _db.SaveData(sql, new { id });
        }

        public List<GenreModel> GetMovieGenres(int id)
        {
            string sql = "SELECT mmd_genres.genre FROM mmd_genres INNER JOIN assign_genre ON mmd_genres.genre_ID = assign_genre.genre_ID WHERE assign_genre.movie_ID = @id";

            List<GenreModel> result = _db.LoadData<GenreModel, dynamic>(sql, new { id });
            
            if (result.Count > 0) return result;
            return null;
        }

        public void CreateAssignGenre(List<GenreModel> genres)
        {
            string sql = "INSERT INTO assign_genre (movie_id, genre_id) VALUES (@Movie_ID, @Genre_ID)";

            _db.SaveData(sql, genres);
        }

    }
}
