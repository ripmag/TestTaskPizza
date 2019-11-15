using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestTaskPizza.Models.Repositories;
using TestTaskPizza.Models;

namespace TestTaskPizza.Controllers
{
    public class DishSelectController : Controller
    {
        UoW db;
        static DishSelect DishSelectView = new DishSelect();        
        public DishSelectController()
        {
            db = new UoW();
        }
        // GET: DishSelect
        public ActionResult Index()
        {
            return View();
        }
        // GET: Dishes/Create
        public ActionResult SelectDish(int id)
        {
            DishSelectView.Ingredients.Clear();
            Dish dish = db.Dishes.Get(id);

            DishSelectView.Name = dish.Name;
            DishSelectView.DishesAmount = 1;
            foreach (Recipe _recipe in dish.Recipes)
            {
                foreach (var prod in db.Products.GetList(1)) //1 - значит продукты которые являются компонентами для пиццы
                {
                    Ingredient customIngredient = new Ingredient() { ProductId = prod.Id, quantity = 0, productName = prod.Name };
                    bool addP = false;
                    //в выбранное блюдо с рецепта переписываем компоненты
                    foreach (Ingredient ingredient in _recipe.Ingredients)
                    {
                        if (ingredient.ProductId == customIngredient.ProductId)
                        {
                            customIngredient.quantity = ingredient.quantity;
                            
                            DishSelectView.Ingredients.Add(customIngredient);
                            addP = true;
                        }
                    }
                    if (!addP)
                        //добавляем продукты (не по рецепту) которые могут использоваться в пицце 
                        DishSelectView.Ingredients.Add(customIngredient);
                }
            }
            DishSelectView.price = GetPrice();
            if (dish.Recipes.Count() == 1)
                DishSelectView.ComboBox = false;
            else
                DishSelectView.ComboBox = true;

            return View(DishSelectView);
        }

        [HttpPost]
        public ActionResult SelectDish(DishSelect ds)
        {

            db.DishSelects.Create(DishSelectView);
            db.Save();
            return RedirectToAction("Create", "Orders");

        }
        public ActionResult DeleteFilling(int id)
        {
            foreach (Ingredient _ingredient in DishSelectView.Ingredients)
            {
                if (_ingredient.ProductId == id)
                    _ingredient.quantity = 0;
            }
            DishSelectView.price = GetPrice();
            return View("SelectDish",DishSelectView);
        }

        public ActionResult AddFilling(int id)
        {
            foreach(Ingredient _ingredient in DishSelectView.Ingredients)
            {
                float step = (float)0.1;
                if (db.Products.Get(id).IsInteger)
                    step = 1;
                if (_ingredient.ProductId == id)                    
                        _ingredient.quantity = _ingredient.quantity + step;                    
            }           
            DishSelectView.price = GetPrice();
            return View("SelectDish", DishSelectView);
        }
        public ActionResult AddDishCount()
        {
            DishSelectView.DishesAmount++;
            DishSelectView.price = GetPrice();
            return View("SelectDish",DishSelectView);
        }        
        public ActionResult SubDishCount()
        {
            if (DishSelectView.DishesAmount > 1)
            {
                DishSelectView.DishesAmount--;
                DishSelectView.price = GetPrice();
            }
            
            return View("SelectDish", DishSelectView);
        }

        public float GetPrice()
        {
            float price = 0;
            
           foreach(var _ingredient in DishSelectView.Ingredients)
                if (_ingredient.quantity>0)
                {
                    price += _ingredient.quantity * db.Products.Get(_ingredient.ProductId.Value).SellPrice;
                }
                return (price*DishSelectView.DishesAmount);
        }
    }
}