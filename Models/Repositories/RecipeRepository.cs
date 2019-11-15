using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TestTaskPizza.Models.Repositories
{
    public class RecipeRepository : IRepository<Recipe>
    {
        private ProductContext _context;

        public RecipeRepository(ProductContext context)
        {
            this._context = context;
        }
        public Recipe Get(int id)
        {
            _context.Recipes
                .Include(p => p.Ingredients)
                .ToList();
            return _context.Recipes.Find(id);
        }
        
        public int Count()
        {
            return _context.Recipes.Count();
        }
        public IEnumerable<Recipe> GetList(int? Group)
        {
            return GetList();
        }
        public IEnumerable<Recipe> GetList()
        {
            return _context.Recipes.ToList();
        }

        public void Create(Recipe item)
        {
            _context.Recipes.Add(item);
        }
        public void Delete(int id)
        {
            Recipe recipe = _context.Recipes
                .Where(c => c.Id == id)
                .FirstOrDefault();
            _context.Entry(recipe)
                .Collection(c => c.Ingredients)
                .Load();
            _context.Recipes.Remove(recipe);
        }

        public void Update(Recipe item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}