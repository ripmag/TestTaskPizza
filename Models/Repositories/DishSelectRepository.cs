using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TestTaskPizza.Models.Repositories
{
    public class DishSelectRepository :IRepository<DishSelect>
    {
        private ProductContext _context;

        public DishSelectRepository(ProductContext context)
        {
            _context = context;
        }
        public DishSelect Get(int id)
        {
            _context.DishSelects.Where(d => d.Id == id)
                .Include(x=>x.Ingredients).Load();
            return _context.DishSelects.Find(id);
        }
        public int Count()
        {
            return _context.DishSelects.Count();
        }
        public IEnumerable<DishSelect> GetList()
        {
            return GetList(null);
        }

        public IEnumerable<DishSelect> GetList(int? Group)
        {
            List<DishSelect> DishesList;
            if (Group == null)
                DishesList = _context.DishSelects.ToList();
            else
                DishesList = _context.DishSelects.Include(d => d.Ingredients)
                   //.Where(o => o.Ingredients.Id == Group)
                   .ToList();
            return DishesList;
        }


        public void Create(DishSelect item)
        {
            _context.DishSelects.Add(item);
        }
        public void Delete(int id)
        {
            DishSelect item = new DishSelect { Id = id };
            _context.Entry(item).State = System.Data.Entity.EntityState.Deleted;
        }

        public void Update(DishSelect item)
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