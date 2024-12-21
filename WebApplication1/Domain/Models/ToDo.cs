using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Domain.Models
{
    [Index(nameof(Title))]
    [Index(nameof(Expiry))]
    public class ToDo
    {
        public int Id { get; set; }
        public DateTime Expiry { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int CompletionPercentage { get; set; }
    }
}