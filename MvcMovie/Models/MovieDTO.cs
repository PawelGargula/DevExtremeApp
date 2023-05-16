using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMovie.Models
{
    public class MovieDTO
    {
        public int Id { get; set; }

        [StringLength(60, MinimumLength = 3), Required]
        public string? Title { get; set; }

        [Display(Name = "Release Date"), DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [RegularExpression(@"[A-Z]+[a-zA-Z\s]*$"), Required, StringLength(30)]
        public string? Genre { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "decimal(18, 2)"), Range(1, 100)]
        public decimal Price { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$"), StringLength(5), Required]
        public string? Rating { get; set; }
        public string? LocalizationName { get; set; }
        public string? OrganizationName { get; set; }
        public string? GroupName { get; set; }
    }
}
