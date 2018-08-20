using MicroServiceCategory.API.Models;
using MicroServiceCategory.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MicroServiceCategory.API.Services
{
    public class CategoryService : ICategoryService
    {
        private static IList<Category> categories = new List<Category>
        {
            new Category { Id = 1, Name = "Brinquedos" },
            new Category { Id = 2, Name = "Celulares" },
            new Category { Id = 3, Name = "Eletrodomésticos" }
        };

        public IList<Category> List(string filter = null)
        {
            if (filter == null)
                return categories;

            return categories.Where(c => c.Name.Contains(filter)).ToList();
        }

        public Category Get(long id)
        {
            return categories.FirstOrDefault(c => c.Id == id);
        }

        public long Post(string name)
        {
            var category = new Category
            {
                Id = categories.Max(c => c.Id) + 1,
                Name = name
            };
            categories.Add(category);
            return category.Id;
        }

        public void Put(long id, string name)
        {
            var category = categories.FirstOrDefault(c => c.Id == id);

            if (category == null)
                throw new ArgumentOutOfRangeException();

            category.Name = name;
        }

        public void Delete(long id)
        {
            categories = categories.Where(c => c.Id != id).ToList();
        }
    }
}