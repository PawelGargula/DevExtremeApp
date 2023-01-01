using Microsoft.AspNetCore.Mvc.Rendering;

namespace MvcMovie.Models
{
    public class MovieGenreViewModel
    {
        public List<Movie>? Movies { get; set; }
        public SelectList? Genres { get; set; }
        public string? MovieGenre { get; set; }
        public string? SearchTitle { get; set; }
        public string? SearchRating { get; set; }
        public DateTime? SearchRelaseDate { get; set; }
    }
}
