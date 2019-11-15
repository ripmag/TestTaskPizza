using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestTaskPizza.Models;
using TestTaskPizza.Models.Repositories;

namespace TestTaskPizza.Controllers
{
    public class GroupController : Controller
    {
        // GET: Group
        UoW db;
        public GroupController()
        {
            db = new UoW();
            
            if (db.Groups.Count()==0)
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
        public ActionResult Index()
        {            
            return View(db.Groups.GetList());
        }


        // GET: Group/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Group/Create
        [HttpPost]
        public ActionResult Create(Group group)
        {
            try
            {
                group.FullName = group.GroupName + ": " + group.SubName;
                db.Groups.Create(group);
                db.Save();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
               
        
        // GET: Group/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Group/Delete/5
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
