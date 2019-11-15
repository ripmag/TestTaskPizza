using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TestTaskPizza.Models
{
    public class Recipe
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Название рецепта")]
        [Required(ErrorMessage = "Пожалуйста, введите название рецепта")]
        [MaxLength(128, ErrorMessage = "Максимальная длинна 128 символа")]
        public string RecipeName { get; set; }

        public virtual ICollection<Ingredient> Ingredients { get; set; }
        public virtual ICollection<Dish> Dishes { get; set; }
        public Recipe()
        {
            Ingredients = new List<Ingredient>();
            Dishes = new List<Dish>();
        }
    }
}