using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TestTaskPizza.Models;
using TestTaskPizza.Models.Repositories;

namespace TestTaskPizza.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        UoW db;

        public ProductController()
        {
            db = new UoW();            
        }
         
        public ActionResult Index()
        {
            return View(db.Products.GetList());
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Product/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create (Product product,HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string g1 = System.Web.Hosting.HostingEnvironment.MapPath("~/") + "Content\\Images\\" + file.FileName;
                    file.SaveAs(g1);
                    product.ImagePath = file.FileName;
                }
                db.Products.Create(product);
                db.Save();
                return RedirectToAction("Index");
            }
            return View(product);
        }


        // GET: Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }            
            Product product = db.Products.Get((int)id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);            
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string g1 = System.Web.Hosting.HostingEnvironment.MapPath("~/") + "Content\\Images\\" + file.FileName;
                    file.SaveAs(g1);
                    product.ImagePath = file.FileName;
                }
                db.Products.Update(product);
                db.Save();

                return RedirectToAction("Index");
            }
            return View(product);

        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            db.Products.Delete(id);
            db.Save();

            return RedirectToAction("Index");
        }

        // POST: Product/Delete/5
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
