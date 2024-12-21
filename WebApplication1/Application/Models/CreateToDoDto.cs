

using System.ComponentModel.DataAnnotations;
using WebApplication1.Application.Validation;

namespace WebApplication1.Application.Models
{
    public class CreateToDoDto
    {
        [Required(ErrorMessage = "The Expiry field is required.")]
        [FutureDate(typeof(CreateToDoDto))]
        public DateTime? Expiry { get; set; }
        [Required(ErrorMessage = "The Title field is required.")]
        [StringLength(50, ErrorMessage = "The Title cannot exceed 50 characters.")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "The Description field is required.")]
        [StringLength(100, ErrorMessage = "The Description cannot exceed 100 characters.")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "The CompletionPercentage field is required.")]
        [Range(0,100, ErrorMessage = "The CompletionPercentage must be between 0 and 100.")]        
        public int? CompletionPercentage { get; set; }
    }
}