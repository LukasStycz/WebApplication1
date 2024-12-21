using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Domain.Logic
{
    public class ToDoUtilities
    {
        public DateTime? ParseToDoExpiry(string date)
        {
            try 
            {
                return DateTime.TryParse(date, out DateTime parsedDate) ? parsedDate.Date : (DateTime?)null;
            }
            catch
            {
                throw;
            }
        }
        public Tuple<DateTime,DateTime> GetStartandEndOfWeek(DateTime date)
        {
            try
            {
                var startOfWeek = date.AddDays(date.DayOfWeek == DayOfWeek.Sunday ? -6 : -(int)date.DayOfWeek + 1);
                var endOfWeek = startOfWeek.AddDays(6);
                return Tuple.Create(startOfWeek,endOfWeek);
            }
            catch
            {
                throw;
            }
        }
        public List<ValidationResult> ValidateToDoTask(object ToDoTaskDto)
        {
            try
            {
                var validationResults = new List<ValidationResult>();
                Validator.TryValidateObject(ToDoTaskDto, new ValidationContext(ToDoTaskDto), validationResults, true);
                return validationResults;
            }
            catch
            {
                throw;
            }
        }
    }
}