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
    public class RecipesController : Controller
    {
        UoW db;
        public RecipesController()
        {
            db = new UoW();
        }
        
        // GET: Recipes
        public ActionResult Index()
        {
            return View(db.Recipes.GetList());
        }

        // GET: Recipes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = new Recipe();

            recipe = db.Recipes.Get((int)id);

            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
        }

        // GET: Recipes/Create
        public ActionResult Create()
        {
            //var products = dbProducts.ToList<Product>();
            var products = db.Products.GetList();
            ViewBag.products = products;
            return View( products);
        }

        // POST: Recipes/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(string RNAME,List<Product> prod,List<float> quantity,List<bool> selected)
        {
            Recipe NewRecipe = new Recipe { RecipeName = RNAME };            
            int index = 0;
            List<Product> _products = (List<Product>) db.Products.GetList();
            
            foreach (var currentProduct in prod)
            {
                if (selected[index])
                {
                    Ingredient NewIngredient = new Ingredient();// { product = currentProduct, quantity = 5 };
                    NewIngredient.ProductId = currentProduct.Id;
                    NewIngredient.quantity = quantity[index];
                    NewIngredient.productName = _products.Find(x=>x.Id == currentProduct.Id).Name;

                    NewRecipe.Ingredients.Add(NewIngredient);
                }
                index++;
            }
            if (RNAME!="" && (NewRecipe.Ingredients.Count()!=0))
            {
                db.Recipes.Create(NewRecipe);
                db.Save();
                return RedirectToAction("Index");
            }
            else
                return View(db.Products.GetList());
        }

        // GET: Recipes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Recipe recipe = db.Recipes.Get(id.Value);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
        }

        // POST: Recipes/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RecipeName")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                db.Recipes.Update(recipe);                
                db.Save();
                return RedirectToAction("Index");
            }
            return View(recipe);
        }

        // GET: Recipes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = db.Recipes.Get(id.Value);            
            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
        }

        // POST: Recipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            db.Recipes.Delete(id);
            db.Save();
            return RedirectToAction("Index");
        }
    }
}
