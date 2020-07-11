using Microsoft.EntityFrameworkCore;
using QuizLogic.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
            var sqlitePath = Path.Combine(System.IO.Directory.GetCurrentDirectory(), @"QuizDb.db");
            optionsBuilder.UseSqlite(@"Data Source=" + sqlitePath);
        }
    }

}
