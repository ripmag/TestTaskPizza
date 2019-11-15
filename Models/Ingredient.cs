using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TestTaskPizza.Models
{
    public class Ingredient
    {        
        public int Id { get; set; }        
        [Display(Name = "Название продукта")]
        [MaxLength(128, ErrorMessage = "Максимальная длинна 128 символа")]
        public string productName { get; set; }
        [Display(Name = "Колличество")]
        public float quantity { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        [Display(Name = "Цена за кг/шт")]
        public float price { get; set; }
        public int? ProductId { get; set; }
        public virtual Product product { get; set; }
        public virtual ICollection<Recipe> Recipes { get; set; }
        public virtual ICollection<DishSelect> DishSelects { get; set; }
        //public int? RecipeId { get; set; }
        //public Recipe Recipe { get; set; }
    }
}