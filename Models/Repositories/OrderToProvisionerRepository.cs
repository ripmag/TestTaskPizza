using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TestTaskPizza.Models.Repositories
{
    public class OrderToProvisionerRepository : IRepository<OrderToProvisioner>
    {
        private ProductContext _context;

        public OrderToProvisionerRepository(ProductContext context)
        {
            this._context = context;
        }
        public OrderToProvisioner Get(int id)
        {
            return _context.OrderToProvisioner.Find(id);
        }
        public int Count()
        {
            return _context.OrderToProvisioner.Count();
        }

        public IEnumerable<OrderToProvisioner> GetList(int? Group)
        {
            return GetList();
        }
        public IEnumerable<OrderToProvisioner> GetList()
        {
            return _context.OrderToProvisioner.ToList();
        }

        public void Create(OrderToProvisioner item)
        {
            _context.OrderToProvisioner.Add(item);
        }
        public void Delete(int id)
        {
            OrderToProvisioner item = new OrderToProvisioner { Id = id };
            _context.Entry(item).State = System.Data.Entity.EntityState.Deleted;
        }

        public void Update(OrderToProvisioner item)
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