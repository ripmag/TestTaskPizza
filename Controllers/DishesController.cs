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
    public class DishesController : Controller
    {
        UoW db;
        static Dish _dish = new Dish(); 
        public DishesController()
        {
            db = new UoW();
            
            if (db.Groups.Count() == 0)
            {
                db.Groups.Create(new Group { GroupName = "Блюдо", SubName = "Пицца", FullName = "Блюдо: Пицца" });
                db.Groups.Create(new Group { GroupName = "Блюдо", SubName = "Соусы", FullName = "Блюдо: Соусы" });
                db.Groups.Create(new Group { GroupName = "Блюдо", SubName = "Салаты", FullName = "Блюдо: Салаты" }); ;
                db.Groups.Create(new Group { GroupName = "Блюдо", SubName = "Горячие блюда", FullName = "Блюдо: Горячие блюда" });
                db.Groups.Create(new Group { GroupName = "Напитки", SubName = "Вода газированная и негазированная", FullName = "Напитки: Вода газированная и негазированная" });
                db.Groups.Create(new Group { GroupName = "Напитки", SubName = "Соки", FullName = "Напитки: Соки" });
                db.Groups.Create(new Group { GroupName = "Готовая продукция", SubName = "Чипсы", FullName = "Готовая продукция: Чипсы" });
                db.Groups.Create(new Group { GroupName = "Готовая продукция", SubName = "Сухарики", FullName = "Готовая продукция: Сухарики" });

                db.Save();
            }
        }
        // GET: Dishes
        public ActionResult Index(int? Group)
        {            
            SelectList allGroups = new SelectList(db.Groups.GetList(), "Id", "FullName");
            ViewData["Groups"] = allGroups;
            
            return View(db.Dishes.GetList(Group));
        }

        // GET: Dishes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Dish dish = db.Dishes.Get((int)id);            
            if (dish == null)
            {
                return HttpNotFound();
            }

            string receptsName = "";
            foreach (var rec in dish.Recipes)
            {
                receptsName += rec.RecipeName + "; ";
            }
            if (dish.Recipes.Count() == 1)
                ViewBag.receptsName = "В состав блюда входит рецепт - " + receptsName;                   
            else
                ViewBag.receptsName = "В состав Комбо-набора входят рецепты: " + receptsName;
            
            //ViewBag.receptsName = receptsName;
            return View(dish);
        }

        // GET: Dishes/Create
        public ActionResult Create()
        {
            SelectList allrecipes = new SelectList (db.Recipes.GetList(), "Id", "RecipeName");            
            SelectList allGroups = new SelectList(db.Groups.GetList(), "Id", "FullName");

            ViewBag.Recipes = allrecipes;
            ViewBag.Groups = allGroups;
            
            if (_dish.Recipes.Count() == 0)
                _dish.Recipes.Add(new Recipe());

            return View(_dish);
        }

        public ActionResult AddRecipe()
        {
            _dish.Recipes.Add(new Recipe());
            return RedirectToAction("Create");
        }
        public ActionResult DeleteRecipe()
        {
            _dish.Recipes.Remove(_dish.Recipes.FirstOrDefault());
            return RedirectToAction("Create");
        }

        // POST: Dishes/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,GroupId")] Dish dish, HttpPostedFileBase file, List<int> Rec)
        {
            foreach (var recID in Rec) //если блюдо состоит из нескольких рецептов (комбо набор)
            {
                Recipe reciep = db.Recipes.Get(recID);
                dish.Recipes.Add(reciep);
            }

            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string g1 = System.Web.Hosting.HostingEnvironment.MapPath("~/") + "Content\\Images\\" + file.FileName;                    
                    file.SaveAs(g1);
                    dish.ImagePath = file.FileName;
                }
                db.Dishes.Create(dish);
                db.Save();
                return RedirectToAction("Index");
            }

            return View(dish);
        }

        // GET: Dishes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dish dish = db.Dishes.Get(id.Value);
            if (dish == null)
            {
                return HttpNotFound();
            }
            return View(dish);
        }

        // POST: Dishes/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Group")] Dish dish)
        {
            if (ModelState.IsValid)
            {
                db.Dishes.Update(dish);
                db.Save();
                return RedirectToAction("Index");
            }
            return View(dish);
        }

        // GET: Dishes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dish dish = db.Dishes.Get(id.Value);
            if (dish == null)
            {
                return HttpNotFound();
            }
            return View(dish);
        }

        // POST: Dishes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            db.Dishes.Delete(id);
            db.Save();
            return RedirectToAction("Index");
        }
    }
}
