using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestTaskPizza.Models
{
    public class OrderToProvisioner
    {
        public int Id { get; set; }         
        [Display(Name = "Сообщение поставщику")]
        [MaxLength(256, ErrorMessage = "Максимальная длинна 256 символа")]
        public string Message { get; set; }
        [Display(Name = "Итоговая сумма предположительной закупки")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public float TotalPrice { get; set; }
        [Display(Name = "Дата создания заявки")]
        public DateTime CreateDate { get; set; }
        public int? ClientId { get; set; }
        public Client Client { get; set; }
        public virtual ICollection<Ingredient> Ingredients { get; set; }
        public OrderToProvisioner()
        {
            Ingredients = new List<Ingredient>();
            CreateDate = DateTime.Now;
        }
    }
}