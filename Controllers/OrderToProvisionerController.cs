using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestTaskPizza.Models;
using TestTaskPizza.Models.Repositories;

namespace TestTaskPizza.Controllers
{
    public class OrderToProvisionerController : Controller
    {
        UoW db;

        public OrderToProvisionerController()
        {
            db = new UoW();
        }
        // GET: OrderToProvisioner
        public ActionResult Index()
        {            
            return View(db.OrdersToProvisioner.GetList());
        }

        // GET: OrderToProvisioner/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        
        // GET: OrderToProvisioner/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderToProvisioner/Delete/5
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
