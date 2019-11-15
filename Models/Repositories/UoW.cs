using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestTaskPizza.Models.Repositories
{
    public class UoW :IDisposable
    {
        private ProductContext db = new ProductContext();

        private ProductRepository _ProductRepository;
        private RecipeRepository _RecipeRepository;
        private DishRepository _DishRepository;
        private DishSelectRepository _DishSelectRepository;
        private GroupRepository _GroupRepository;
        private OrderRepository _OrderRepository;
        private OrderToProvisionerRepository _OrderToProvisionerRepository;
        private ProductArrivalRepository _ProductArrivalRepository;
        private ClientRepository _ClientRepository;

        private bool disposed = false;

        public ProductRepository Products
        {
            get
            {
                if (_ProductRepository == null)
                    _ProductRepository = new ProductRepository(db);
                return _ProductRepository;
            }
        }
        public RecipeRepository Recipes
        {
            get
            {
                if (_RecipeRepository == null)
                    _RecipeRepository = new RecipeRepository(db);
                return _RecipeRepository;
            }
        }
        public DishRepository Dishes
        {
            get
            {
                if (_DishRepository == null)
                    _DishRepository = new DishRepository(db);
                return _DishRepository;
            }
        }
        public DishSelectRepository DishSelects
        {
            get
            {
                if (_DishSelectRepository == null)
                    _DishSelectRepository = new DishSelectRepository(db);
                return _DishSelectRepository;
            }
        }
        public OrderRepository Orders
        {
            get
            {
                if (_OrderRepository == null)
                    _OrderRepository = new OrderRepository(db);
                return _OrderRepository;
            }
        }
        public GroupRepository Groups
        {
            get
            {
                if (_GroupRepository == null)
                    _GroupRepository = new GroupRepository(db);
                return _GroupRepository;
            }
        }
        public OrderToProvisionerRepository OrdersToProvisioner
        {
            get
            {
                if (_OrderToProvisionerRepository == null)
                    _OrderToProvisionerRepository = new OrderToProvisionerRepository(db);
                return _OrderToProvisionerRepository;
            }
        }
        public ProductArrivalRepository ProductsArrival
        {
            get
            {
                if (_ProductArrivalRepository == null)
                    _ProductArrivalRepository = new ProductArrivalRepository(db);
                return _ProductArrivalRepository;
            }
        }
        public ClientRepository Clients
        {
            get
            {
                if (_ClientRepository == null)
                    _ClientRepository = new ClientRepository(db);
                return _ClientRepository;
            }
        }


        public void Save()
        {
            db.SaveChanges();
        }
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}