using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TestTaskPizza.Models
{
    public class Order
    {       
        public int Id { get; set; }         
        [Display(Name = "Блюдо")]
        [MaxLength(128, ErrorMessage = "Максимальная длинна 128 символа")]
        public string DishName { get; set; }
        [Display(Name = "Цена")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public float DishPrice { get; set; }
        [Display(Name = "Колл-во")]
        public int DishAmount { get; set; }
        [Display(Name = "Дата заказа")]
        public DateTime CreateDate { get; set; }
        public virtual ICollection<Ingredient> IngredientsTotal { get; set; }
        public int? DishSelectId { get; set; }
        public virtual ICollection<Ingredient> DishSelect { get; set; }
        public int? ClientId { get; set; }
        public virtual Client Client { get; set; }
        public Order()
        {
            //Dish = new Dish();
            //Client = new Client();
            //Client.Name = "Name";
            //Client.NumberPhone = "+38";
            //Client.email = "client@gmail.com";

            IngredientsTotal = new List<Ingredient>();
            CreateDate = DateTime.Now;
        }
    }
}