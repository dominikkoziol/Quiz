using Microsoft.EntityFrameworkCore;
using QuizLogic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuizLogic.Context
{
    public class QuizEntityContext : DbContext
    {
    
        public DbSet<Question> Questions { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=QuizDb;Trusted_Connection=True;");
        }
    }

}
