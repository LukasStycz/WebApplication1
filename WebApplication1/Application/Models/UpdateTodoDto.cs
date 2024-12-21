
using System.ComponentModel.DataAnnotations;


namespace WebApplication1.Application.Models
{
    public class UpdateTodoDto : CreateToDoDto
    {
        [Required(ErrorMessage = "The Id field is required.")]
        public int? id { get; set; }
    }
}