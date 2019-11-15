using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TestTaskPizza.Models.Repositories
{
    public class ProductArrivalRepository : IRepository<ProductArrival>
    {
        private ProductContext _context;

        public ProductArrivalRepository(ProductContext context)
        {
            this._context = context;
        }
        public ProductArrival Get(int id)
        {
            return _context.ProductArrival.Find(id);
        }
        public int Count()
        {
            return _context.ProductArrival.Count();
        }

        public IEnumerable<ProductArrival> GetList(int? Group)
        {
            return GetList();
        }
        public IEnumerable<ProductArrival> GetList()
        {
            return _context.ProductArrival
                .Include(x => x.Ingredient)
                .ToList();
        }

        public void Create(ProductArrival item)
        {
            _context.ProductArrival.Add(item);
        }
        public void Delete(int id)
        {
            ProductArrival item = new ProductArrival { Id = id };
            _context.Entry(item).State = System.Data.Entity.EntityState.Deleted;
        }

        public void Update(ProductArrival item)
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