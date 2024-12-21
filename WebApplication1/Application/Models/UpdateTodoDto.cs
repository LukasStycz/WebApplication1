using System.ComponentModel.DataAnnotations;

//Class that holds incoming data and is also responsible for validating data that is not required in the parent class.
namespace WebApplication1.Application.Models
{
    public class UpdateTodoDto : CreateToDoDto
    {
        [Required(ErrorMessage = "The Id field is required.")]
        public int? Id { get; set; }
    }
}