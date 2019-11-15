using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TestTaskPizza.Models.Repositories
{
    public class GroupRepository :IRepository<Group>
    {
        private ProductContext _context;

        public GroupRepository(ProductContext context)
        {
            this._context = context;
        }
        public Group Get(int id)
        {
            return _context.Groups.Find(id);
        }
        public int Count()
        {
            return _context.Groups.Count();
        }

        public IEnumerable<Group> GetList(int? Group)
        {
            return GetList();
        }
        public IEnumerable<Group> GetList()
        {
            return _context.Groups.ToList();
        }

        public void Create(Group item)
        {
            _context.Groups.Add(item);
        }
        public void Delete(int id)
        {
            Group item = new Group { Id = id };
            _context.Entry(item).State = System.Data.Entity.EntityState.Deleted;
        }

        public void Update(Group item)
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