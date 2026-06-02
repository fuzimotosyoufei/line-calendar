using Microsoft.EntityFrameworkCore;
using ShiftManagerApi.Models;

namespace ShiftManagerApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> options) : base(options) //要解読
        { 
        }
        
        public DbSet<ShiftEntity> Shifts { get; set; }
    }
}
