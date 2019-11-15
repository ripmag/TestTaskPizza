using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TestTaskPizza.Models.Repositories
{
    public class ClientRepository :IRepository<Client>
    {
        private ProductContext _context;

        public ClientRepository(ProductContext context)
        {
            this._context = context;
        }
        public Client Get(int id)
        {
            return _context.Clients.Find(id);
        }
        public int Count()
        {
            return _context.Clients.Count();
        }
        public IEnumerable<Client> GetList(int? Group)
        {
            return GetList();
        }
        public IEnumerable<Client> GetList()
        {
            return _context.Clients.ToList();
        }

        public void Create(Client item)
        {
            _context.Clients.Add(item);
        }
        public void Delete(int id)
        {
            Order item = _context.Orders
               .Where(c => c.Client.Id==id)
               .FirstOrDefault();
            if (item != null)
            {
                _context.Entry(item)
                    .Collection(c => c.IngredientsTotal)
                    .Load();
                _context.Orders.Remove(item);
            }
            Client itemC = new Client { Id = id };
           _context.Entry(itemC).State = System.Data.Entity.EntityState.Deleted;
        }

        public void Update(Client item)
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