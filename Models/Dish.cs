using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestTaskPizza.Models
{
    public class Dish
    {
        //[Key]
        public int Id { get; set; }
        [Display(Name = "Название блюда")]
        [Required(ErrorMessage = "Пожалуйста, введите название блюда")]
        [MaxLength(128, ErrorMessage = "Максимальная длинна 128 символа")]
        public string Name { get; set; }

        [Display(Name = "Название рецепта")]
        [MaxLength(128, ErrorMessage = "Максимальная длинна 128 символа")]
        public string DishRecipeName { get; set; }
        [Display(Name = "Изображение")]
        public string ImagePath { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public float price { get; set; }        
        public int? GroupId { get; set; }
        [Display(Name = "Категория")]
        public virtual Group Group { get; set; }
        [Display(Name = "Рецепт")]

        public virtual ICollection<Recipe> Recipes { get; set; }

        public Dish()
        {
            //Список рецептов для комбо-набора
            Recipes = new List<Recipe>();
        }
    }
}