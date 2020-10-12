using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace PlusNarrativeAssessment.Data
{
    public class DataContext  : DbContext
    {
        public DataContext()
        {
        }

        public DbSet<Game> Games { get; set; }

        public DbSet<PlayerAnswer> PlayerAnswers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer("Server=localhost;Database=MovieNightTrivia;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}
