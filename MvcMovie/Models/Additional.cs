using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
    public class Additional
    {
        public int Id { get; set; }
        public string? Detail1 { get; set; }
        public string? Detail2 { get; set; }
        public int MovieId { get; set; }
    }
}
