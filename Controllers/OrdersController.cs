using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TestTaskPizza.Models;
using TestTaskPizza.Models.Repositories;

namespace TestTaskPizza.Controllers
{
    public class OrdersController : Controller
    {
        UoW db;        
        static Order _order = new Order();
        OrderToProvisioner _OrderTP = new OrderToProvisioner();
        public OrdersController()
        {
            db = new UoW();
        }
        // GET: Orders
        public ActionResult Index()
        {            
            return View(db.Orders.GetList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Get((int)id);
            
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {            
            DishSelect dishSelect = db.DishSelects.Get(db.DishSelects.Count());
            _order.DishPrice = dishSelect.price;
            _order.DishAmount = dishSelect.DishesAmount;
            _order.DishName = dishSelect.Name;          

            return View(_order);
        }

        public ActionResult Buy(Order _ord)
        {
            DishSelect dishSelect = db.DishSelects.Get(db.DishSelects.Count());
            _ord.DishSelectId = dishSelect.Id;
            _ord.DishName = dishSelect.Name;
            _ord.DishAmount = dishSelect.DishesAmount;
            _ord.DishPrice = dishSelect.price;
            var ClientsList = db.Clients.GetList();
            foreach (var client in ClientsList)
            {
                if ((client.email == _ord.Client.email) && (client.NumberPhone == _ord.Client.NumberPhone))
                {
                    _ord.ClientId = client.Id;
                    _ord.Client = null;
                    break;
                }
            }           

            //Вычитаем продукты со склада 
            foreach (Ingredient _ingredient in dishSelect.Ingredients)
            {
                Product p = db.Products.Get((int)_ingredient.ProductId);
                p.Balance = p.Balance - (_ingredient.quantity * dishSelect.DishesAmount);
                db.Products.Update(p);
            }
            //Если превышен лимит минимального остатка продуктов => формируем заказ
            
            foreach (Ingredient _ingredient in dishSelect.Ingredients)
            {
                Product p = db.Products.Get((int)_ingredient.ProductId);
                if (p.Balance < p.MinBalance)
                {
                    _OrderTP.Ingredients.Add(new Ingredient() { price = p.SellPrice, ProductId = p.Id,productName = p.Name, quantity = p.MinBalance});
                    _OrderTP.Message += "Закончился товар - " + p.Name + ", везите ещё;";
                }
            }
            _OrderTP.TotalPrice = GetTotalPrice();
            if (_OrderTP.Ingredients.Count() > 0)
                db.OrdersToProvisioner.Create(_OrderTP);

            db.Orders.Create(_ord);
            db.Save();
            return RedirectToAction("Index");
        }
        public float GetTotalPrice()
        {
            float TP = 0;
            foreach (var _ingredient in _OrderTP.Ingredients)
            {
                TP += _ingredient.quantity * _ingredient.price;
            }
            return TP;
        }

        // POST: Orders/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DishName,price,CreateDate")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Create(order);
                db.Save();
                return RedirectToAction("Index");
            }

            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Get((int)id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DishName,price,CreateDate")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Update(order);
                db.Save();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Get((int)id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            db.Orders.Delete(id);
            db.Save();
            return RedirectToAction("Index");
        }
    }
}
