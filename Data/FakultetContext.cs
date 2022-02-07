using Fakultet.Models;
using Microsoft.EntityFrameworkCore;

namespace Fakultet.Data
{
    public class FakultetContext : DbContext
    {
        public FakultetContext(DbContextOptions<FakultetContext> opt) : base(opt)
        {

        }

        public DbSet<Predmet> Predmeti { get; set; }
        public DbSet<Student> Studenti { get; set; }
        public DbSet<StudentPredmet> StudentPredmet { get; set; }
    }
}