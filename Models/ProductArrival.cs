using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestTaskPizza.Models
{
    public class ProductArrival
    {
        public int Id { get; set; }
        public Ingredient Ingredient { get; set; }
        [Display(Name = "Дата получения продукта")]
        public DateTime ArrivalDate { get; set; }
        public ProductArrival()
        {            
            ArrivalDate = DateTime.Now;
        }
    }
}