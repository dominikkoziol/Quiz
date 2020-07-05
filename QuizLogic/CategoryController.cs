using Microsoft.EntityFrameworkCore;
using QuizLogic.Context;
using QuizLogic.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuizLogic
{
    public static class CategoryController
    {
        public static async Task<List<Category>> GetCategories()
        {
            using(var context = new QuizEntityContext())
            {
                List<Category> categories = await context.Categories.ToListAsync();
                return categories;
            }
        
        }
    }
}
