
using WebApplication1.Domain.Logic;

namespace TestProject1
{
    public class ParseToDoExpiryTest
    {
        [Theory]
        [InlineData("2024-12-22", "2024-12-22")]
        [InlineData("2024-12-22 15:30", "2024-12-22")]
        public void ParseToDoExpiry_ShouldReturnDate_WhenValidStringIsProvided(string inputDate, string expectedDate)
        {
            //Arrange
            var utilities = new ToDoUtilities();
            var expectedResult = DateTime.Parse(expectedDate);
            //Act
            var result = utilities.ParseToDoExpiry(inputDate);
            //Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResult, result);
            }
            [Theory]
            [InlineData("invalid date")]
            [InlineData("2024-12-33")]
            public void ParseToDoExpiry_ShouldReturnNull_WheninInvalidStringIsProvided(string inputDate)
            {
            //Arrange
            var ParseToDoExpiry = new ToDoUtilities();
            //Act
            var result = ParseToDoExpiry.ParseToDoExpiry(inputDate);
            //Assert
            Assert.Null(result);
        }
    }
}