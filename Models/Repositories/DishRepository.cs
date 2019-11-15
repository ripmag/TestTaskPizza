using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TestTaskPizza.Models.Repositories
{
    public class DishRepository :IRepository<Dish>
    {
        private ProductContext _context;

        public DishRepository(ProductContext context)
        {
            this._context = context;
        }
        public Dish Get(int id)
        {
            _context.Dishes.Where(d => d.Id == id)
                //.Include(d => d.)
                //.Include(d => d.DishRecipes.Select(t => t.Ingredients))
                .Load();
            return _context.Dishes.Find(id);
        }
        public int Count()
        {
            return _context.Dishes.Count();
        }
        public IEnumerable<Dish> GetList()
        {
            return GetList(null);
        }
        
            public IEnumerable<Dish> GetList(int? Group)
        {
            List<Dish> DishesList;
            if (Group == null)
                DishesList = _context.Dishes.ToList();
            else
                DishesList = _context.Dishes
                   .Where(o => o.GroupId == Group)
                   .ToList();
            

            foreach(Dish _Dish in DishesList)
            {
                float price = 0;
                foreach (Recipe _Recipe in _Dish.Recipes)
                {
                    foreach (Ingredient _Ingredient in _Recipe.Ingredients)
                        if (_Ingredient.quantity > 0)
                        {
                            price += _Ingredient.quantity * _context.Products.Find(_Ingredient.ProductId).SellPrice;
                        }
                }
                _Dish.price = price;
            }
            return DishesList;
        }


        public void Create(Dish item)
        {
            _context.Dishes.Add(item);
        }
        public void Delete(int id)
        {
            Dish item = new Dish { Id = id };
            _context.Entry(item).State = System.Data.Entity.EntityState.Deleted;
        }

        public void Update(Dish item)
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