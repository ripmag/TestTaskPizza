using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestTaskPizza.Models;
using TestTaskPizza.Models.Repositories;

namespace TestTaskPizza.Controllers
{
    public class ProductArrivalController : Controller
    {
        // GET: ProductArrival
        UoW db;
        public ProductArrivalController()
        {
            db = new UoW();
        }
        public ActionResult Index()
        {            
            return View(db.ProductsArrival.GetList());
        }

        // GET: ProductArrival/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductArrival/Create
        public ActionResult Create()
        {
            SelectList allproduct = new SelectList(db.Products.GetList(), "Id", "Name");
            ViewBag.allproduct = allproduct;
            return View();
        }

        // POST: ProductArrival/Create
        [HttpPost]
        public ActionResult Create(ProductArrival pa)
        {
            try
            {
                Product _prod = db.Products.Get(pa.Ingredient.Id);
                if (_prod != null) //Если уже есть такой продукт
                {//пересчет цены товара
                    float newBalance = _prod.Balance + pa.Ingredient.quantity;
                    float newSellPrice = ( (_prod.Balance * _prod.SellPrice) + (pa.Ingredient.quantity * pa.Ingredient.price) )
                                                                            /newBalance;
                    _prod.Balance = newBalance;
                    _prod.SellPrice = newSellPrice;
                    pa.Ingredient.productName = _prod.Name;
                    db.Products.Update(_prod);
                    //db.Entry(_prod).State = EntityState.Modified;
                }
                else
                {
                    _prod.MinBalance = 5;
                    _prod.Name = pa.Ingredient.productName;
                    _prod.Balance = pa.Ingredient.quantity;
                    db.Products.Create(_prod);
                }
                db.ProductsArrival.Create(pa);
                db.Save();                

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
               
        // GET: ProductArrival/Delete/5
        public ActionResult Delete(int id)
        {
            db.ProductsArrival.Delete(id);
            db.Save();            
            return RedirectToAction("Index");
        }

        // POST: ProductArrival/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
