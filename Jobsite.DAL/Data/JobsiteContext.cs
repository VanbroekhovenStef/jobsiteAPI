using Jobsite.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobsite.DAL.Data
{
    public class JobsiteContext : DbContext
    {
        public JobsiteContext(DbContextOptions<JobsiteContext> options) : base(options) {  }
   
        public DbSet<User> Users { get; set; }
        public DbSet<Vacature> Vacatures { get; set; }
        public DbSet<Sollicitatie> Sollicitaties { get; set; }
        public DbSet<Bedrijf> Bedrijven { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Vacature>().ToTable("Vacature");
            modelBuilder.Entity<Sollicitatie>().ToTable("Sollicitatie").HasOne(e => e.Vacature).WithMany(x => x.Sollicitaties).HasForeignKey(s => s.VacatureId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Bedrijf>().ToTable("Bedrijf");
        }
    }
}
