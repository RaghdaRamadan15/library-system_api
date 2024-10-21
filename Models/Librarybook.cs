using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace day2.Models
{
    public class Librarybook:DbContext
    {


       public DbSet<Author>Authors { get; set; }
        public DbSet<book> books { get; set; }
        public DbSet<labAuthor> labAuthors { get; set; }
        public DbSet<lab> lab { get; set; }
        public Librarybook(DbContextOptions opions) : base(opions)
        {

        }

    }
}
