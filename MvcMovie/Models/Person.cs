using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
    public class Person
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Please, use letters in the first name. Digits are not allowed.")]
        [StringLength(int.MaxValue, MinimumLength = 2, ErrorMessage = "First name must have at least 2 characters")]
        public string? FirstName { get; set; }

        //[Range(typeof(DateTime), "1/1/1901", "1/1/2016")]
        public DateTime BirthDate { get; set; }

        //[Remote("CheckEmailAddress", "Validation")]
        public string? Email { get; set; }
        
        public int Localization { get; set; }
    }
}
