
using WebApplication1.Domain.Logic;

namespace TestProject1;

public class GetStartandEndOfWeekTest
{
    [Theory]
    [InlineData("2024-12-22", "2024-12-16", "2024-12-22")]
    [InlineData("2024-12-24", "2024-12-23", "2024-12-29")]
    [InlineData("2024-12-09", "2024-12-09", "2024-12-15")]
    [InlineData("2025-01-01", "2024-12-30", "2025-01-05")]
    public void GetStartandEndOfWeek_ShouldReturnCorrectStartAndEndOfWeek(string inputDate, string expectedStartOfWeek, string expectedEndOfWeek)
    {
        //Arrange
        var utilities = new ToDoUtilities();
        var date = DateTime.Parse(inputDate);
        var expectedStart = DateTime.Parse(expectedStartOfWeek);
        var expectedEnd = DateTime.Parse(expectedEndOfWeek);
        //Act
        var result = utilities.GetStartandEndOfWeek(date);
        //Assert
        Assert.Equal(expectedStart.Date, result.Item1.Date);
        Assert.Equal(expectedEnd.Date, result.Item2.Date);

    }
}