using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace TestTaskPizza.Models
{
    public class ProductRepository : IRepository<Product>
    {
        private ProductContext _context;

        public ProductRepository (ProductContext context)
        {
            this._context = context;
        }
        public Product Get(int id)
        {
            return _context.Products.Find(id);
        }
        public int Count()
        {
            return _context.Products.Count();
        }

        public IEnumerable<Product> GetList(int? Group)
        {
            if (Group == 1)
                return _context.Products.Where(p => p.IsFilling == true).ToList();
            else
            return GetList();
        }
        public IEnumerable<Product> GetList()
        {
            return _context.Products.ToList();
        }

        public void Create(Product item)
        {
            _context.Products.Add(item);            
        }
        public void Delete(int id)
        {
            Product item = new Product { Id = id };
            _context.Entry(item).State = System.Data.Entity.EntityState.Deleted;
        }

        public void Update(Product item)
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