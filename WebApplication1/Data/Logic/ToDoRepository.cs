using Microsoft.EntityFrameworkCore;
using WebApplication1.Data.Context;
using WebApplication1.Domain.Models;

//Class responsible for database interaction.
namespace WebApplication1.Data.Logic
{
    public class ToDoRepository(ToDoDbContext dbContext)
    {
        private readonly ToDoDbContext _dbContext = dbContext; 
        public async Task<List<ToDo>> GetAllToDosAsync()
        {
            try
            {
                return await (_dbContext.ToDos?.ToListAsync() ?? Task.FromResult(new List<ToDo>()));
            }
            catch
            {
                throw;
            }
        } 

        public async Task<ToDo?> GetToDoByIdAsync(int id)
        {
            try
            {
                return await _dbContext.ToDos!.FindAsync(id);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<ToDo>> GetToDoByTitleAsync(string title)
        {
            try
            {
                return await _dbContext.Set<ToDo>()
                    .Where(x => x.Title == title)
                    .ToListAsync();
            }
            catch
            {
                throw;
            }
        }   

        public async Task<List<ToDo>> GetToDoFromOneDayAsync(DateTime? date)
        {
            try
            {
                return await _dbContext.Set<ToDo>()
                    .Where(x => x.Expiry.Date == date)
                    .ToListAsync();
            }
            catch
            {
                throw;
            }
        } 

        public async Task<List<ToDo>> GetToDoFromWeekAsync(Tuple<DateTime,DateTime> week)
        {
            try
            {
                return await _dbContext.Set<ToDo>()
                    .Where(x => x.Expiry.Date >= week.Item1 && x.Expiry.Date <= week.Item2)
                    .ToListAsync();
            }
            catch
            {
                throw;
            }
        }   

        public async Task<ToDo> InsertToDoAsync(ToDo toDo)
        {
            try
            {
                _dbContext.ToDos!.Add(toDo);
                await _dbContext.SaveChangesAsync();
                return toDo;
            }
            catch
            {
                throw;
            }
        }

        public async Task<ToDo?> UpdateToDoAsync(ToDo toDo)
        {
            try
            {
                var exsistingToDo = await _dbContext.ToDos!.FindAsync(toDo.Id);
                if (exsistingToDo == null)
                {
                    return null;
                }
                else
                {
                    exsistingToDo.Expiry = toDo.Expiry;
                    exsistingToDo.Title = toDo.Title;
                    exsistingToDo.Description = toDo.Description;
                    exsistingToDo.CompletionPercentage = toDo.CompletionPercentage;
                    await _dbContext.SaveChangesAsync();
                    return exsistingToDo;
                }

            }
            catch
            {
                throw;
            }
        }   

        public async Task<ToDo?> UpdateToDoPrecentageAsync(int id, int percentage)
        {
            try
            {
                var exsistingToDo = await _dbContext.ToDos!.FindAsync(id);
                if (exsistingToDo == null)
                {
                    return null;
                }
                else
                {
                    exsistingToDo.CompletionPercentage = percentage;
                    await _dbContext.SaveChangesAsync();
                    return exsistingToDo;
                }

            }
            catch
            {
                throw;
            }
        }  

        public async Task<bool> DeleteToDoByIdAsync(int id)
        {
            try
            {
                var exsistingToDo = await _dbContext.ToDos!.FindAsync(id);
                if (exsistingToDo == null)
                {
                    return false;
                }
                else
                {
                    _dbContext.ToDos!.Remove(exsistingToDo);
                    await _dbContext.SaveChangesAsync();
                    return await _dbContext.ToDos!.FindAsync(id) == null;
                }

            }
            catch
            {
                throw;
            }
        }
        
    }
}