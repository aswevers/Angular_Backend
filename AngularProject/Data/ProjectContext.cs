using AngularProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularProject.Data
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options) { }
        public DbSet<Stem> Stemmen { get; set; }
        public DbSet<Keuze> Keuzes { get; set; }
        public DbSet<Poll> Polls { get; set; }
        public DbSet<PollGebruiker> PollGebruikers { get; set; }
        public DbSet<Vriend> Vrienden { get; set; }
        public DbSet<Gebruiker> Gebruikers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder) { 
            modelBuilder.Entity<Stem>().ToTable("Stem"); 
            modelBuilder.Entity<Keuze>().ToTable("Keuze"); 
            modelBuilder.Entity<Poll>().ToTable("Poll");
            modelBuilder.Entity<Gebruiker>().ToTable("Gebruiker");
            modelBuilder.Entity<PollGebruiker>().ToTable("PollGebruiker");
            modelBuilder.Entity<Vriend>().ToTable("Vriend"); 

        }
    }
}
