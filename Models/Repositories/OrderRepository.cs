using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TestTaskPizza.Models.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        private ProductContext _context;

        public OrderRepository(ProductContext context)
        {
            this._context = context;
        }
        public Order Get(int id)
        {
            _context.Orders.Include(o => o.Client).Load();
            return _context.Orders.Find(id);
        }
        public int Count()
        {
            return _context.Orders.Count();
        }

        public IEnumerable<Order> GetList(int? Group)
        {
            return GetList();
        }
        public IEnumerable<Order> GetList()
        {            
            return _context.Orders
                .Include(o => o.Client)
                .ToList();
        }

        public void Create(Order item)
        {
            _context.Orders.Add(item);
        }
        public void Delete(int id)
        {
            Order item = new Order { Id = id };
            _context.Entry(item).State = System.Data.Entity.EntityState.Deleted;
        }

        public void Update(Order item)
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