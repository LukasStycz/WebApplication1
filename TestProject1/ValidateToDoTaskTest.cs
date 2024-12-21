using WebApplication1.Application.Models;
using WebApplication1.Domain.Logic;

namespace TestProject1
{
    public class ValidateToDoTaskTest
    {
        [Fact]
        public void ValidateToDoTask_ShouldReturnValidationErrors_When_CreateToDoDto_RequiredFieldsAreMissing()
        {
            //Arrange
            var utilities = new ToDoUtilities();
            var dto = new CreateToDoDto();
            //Act
            var results = utilities.ValidateToDoTask(dto);
            //Assert
            Assert.Equal(4, results.Count);
            Assert.Contains(results, r => r.ErrorMessage == "The Expiry field is required.");
            Assert.Contains(results, r => r.ErrorMessage == "The Title field is required.");
            Assert.Contains(results, r => r.ErrorMessage == "The Description field is required.");
            Assert.Contains(results, r => r.ErrorMessage == "The CompletionPercentage field is required.");
        }
        [Fact]
        public void ValidateToDoTask_ShouldReturnValidationErrors_WhenInvalidDataIsProvided()
        {
            //Arrange
            var utilities = new ToDoUtilities();
            var dto = new CreateToDoDto
            {
                Expiry = DateTime.Now.AddDays(-1),
                Title = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                CompletionPercentage = 150
            };
            //Act
            var results = utilities.ValidateToDoTask(dto);
            //Assert
            Assert.Equal(4, results.Count);
            Assert.Contains(results, r => r.ErrorMessage == "The date must be in the future.");
            Assert.Contains(results, r => r.ErrorMessage == "The Title cannot exceed 50 characters.");
            Assert.Contains(results, r => r.ErrorMessage == "The Description cannot exceed 100 characters.");
            Assert.Contains(results, r => r.ErrorMessage == "The CompletionPercentage must be between 0 and 100.");
        }
        [Fact]
        public void ValidateToDoTask_ShouldReturnValidationErrors_When_UpdateToDoDto_RequiredFieldsAreMissing()
        {
            //Arrange
            var utilities = new ToDoUtilities();
            var dto = new UpdateTodoDto();
            //Act
            var results = utilities.ValidateToDoTask(dto);
            //Assert
            Assert.Equal(5, results.Count);
            Assert.Contains(results, r => r.ErrorMessage == "The Id field is required.");
            Assert.Contains(results, r => r.ErrorMessage == "The Expiry field is required.");
            Assert.Contains(results, r => r.ErrorMessage == "The Title field is required.");
            Assert.Contains(results, r => r.ErrorMessage == "The Description field is required.");
            Assert.Contains(results, r => r.ErrorMessage == "The CompletionPercentage field is required.");
        }
    }
}