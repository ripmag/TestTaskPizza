using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace TestTaskPizza.Models
{
    public class Product
    {        
        public int Id { get; set; }
        [Display(Name = "Название продукта")]
        [Required(ErrorMessage = "Пожалуйста, введите название продукта")]
        [MaxLength(64, ErrorMessage = "Максимальная длинна 64 символа")]
        public string Name { get; set; }
        [Display(Name = "Минимальный остаток")]
        [Required(ErrorMessage = "Пожалуйста, введите минимальный остаток")]
        public float MinBalance { get; set; }
        [Display(Name = "Сколько на складе (шт/кг)")]
        [Required(ErrorMessage = "Пожалуйста, введите колличество на складе")]
        public float Balance { get; set; }
        [Display(Name = "Цена продажи")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Пожалуйста, введите цену продукта")]
        public float SellPrice { get; set; }
        [Display(Name = "Колличество в шт")]
        public bool IsInteger { get; set; }
        [Display(Name = "Продукт является начинкой для пиццы")]
        public bool IsFilling { get; set; }
        [Display(Name = "Фото продукта")]
        public string ImagePath { get; set; }

    }
}