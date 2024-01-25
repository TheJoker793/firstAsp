using firstAsp.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace firstAsp.Data
{
    public class FirstDbContext:DbContext
    {
        public FirstDbContext(DbContextOptions dbContextOptions):base(dbContextOptions)
        {
            
        }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> regions { get; set; }
        public DbSet<Walk> Walks { get; set; }

        internal static Task SavesChange()
        {
            throw new NotImplementedException();
        }
    }
}
