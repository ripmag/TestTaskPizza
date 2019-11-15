using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestTaskPizza.Models
{
    public class Group
    {
        //[Key]
        public int Id { get; set; }
        [Display(Name = "Категория")]
        [Required(ErrorMessage = "Пожалуйста, введите название категории")]
        [MaxLength(128, ErrorMessage = "Максимальная длинна 128 символа")]
        public string GroupName { get; set; }
        [Display(Name = "Подкатегория")]
        [Required(ErrorMessage = "Пожалуйста, введите название подкатегории")]
        [MaxLength(128, ErrorMessage = "Максимальная длинна 128 символа")]
        public string SubName { get; set; }
        [Display(Name = "Категория")]
        public string FullName { get; set; }
        public virtual ICollection<Dish> Dishes { get; set; }
        /*public Group()
        {
            Dishes = new List<Dish>();
        }*/
    }
}