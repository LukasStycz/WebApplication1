using Microsoft.EntityFrameworkCore;
using WebApplication1.Domain.Models;

namespace WebApplication1.Data.Context
{
    public class ToDoDbContext(DbContextOptions<ToDoDbContext> options) : DbContext(options)
    {
        public DbSet<ToDo>? ToDos { get; set; }
        public void Seed()
        {
            if (!ToDos!.Any()) // Sprawdzamy, czy istnieją rekordy w tabeli ToDos
            {
                ToDos!.AddRange(
                    new ToDo
                    {
                        Id = 1,
                        Expiry = DateTime.Now,
                        Title = "Buy groceries",
                        Description = "Buy groceries",
                        CompletionPercentage = 10
                    },
                    new ToDo
                    {
                        Id = 2,
                        Expiry = DateTime.Now,
                        Title = "Buy groceries2",
                        Description = "Buy groceries2",
                        CompletionPercentage = 20
                    },
                    new ToDo
                    {
                        Id = 3,
                        Expiry = DateTime.Now,
                        Title = "Buy groceries3",
                        Description = "Buy groceries3",
                        CompletionPercentage = 30
                    }
                );
                SaveChanges(); // Zapisujemy zmiany do bazy danych
            }
        }
    }
}