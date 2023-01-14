using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
    public class DictionaryDefinition
    {
        public int Id { get; set; }
        [Display(Name = "Kod"), Required(ErrorMessage = "Kod jest wymagany")]
        public string? Code { get; set; }
        [Display(Name = "Nazwa"), Required(ErrorMessage = "Nazwa jest wymagana")]
        public string? Name { get; set; }
        public bool IsSystemic { get; set; }
    }
}
