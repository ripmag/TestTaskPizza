using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestTaskPizza.Models
{
    public class DishSelect
    {
        [Key]
        public int Id{ get; set; }
        [Display(Name = "Название блюда")]
        [Required(ErrorMessage = "Пожалуйста, введите название блюда")]
        [MaxLength(128, ErrorMessage = "Максимальная длинна 128 символа")]
        public string Name { get; set; }

        public ICollection<Ingredient> Ingredients { get; set; }
        [Display(Name = "Цена")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public float price { get; set; }
        [Display(Name = "Колличество блюд")]
        public int DishesAmount { get; set; }
        public bool ComboBox { get; set; }
       
        public DishSelect()
        {
            Ingredients = new List<Ingredient>();
        }

    }
}