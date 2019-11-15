using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestTaskPizza.Models
{
    public class Client
    {
        public int Id { get; set; }
        [Display(Name = "Клиент:")]
        public ClientType clientType { get; set; }
        [Display(Name = "Ф.И.О.")]
        [Required(ErrorMessage = "Пожалуйста, введите Ф.И.О.")]
        [MaxLength(64, ErrorMessage = "Максимальная длинна 64 символа")]
        public string Name { get; set; }
        [Display(Name = "Номер телефона")]
        [Required(ErrorMessage = "Пожалуйста, номер телефона")]
        [MaxLength(64, ErrorMessage = "Максимальная длинна 64 символа")]
        public string NumberPhone { get; set; }
        [Display(Name = "Почта")]
        [Required(ErrorMessage = "Пожалуйста, введите почту")]
        [MaxLength(256, ErrorMessage = "Максимальная длинна 256 символа")]
        public string email { get; set; }
        
    }
    public enum ClientType
    {
        buyer = 0,
        provisioner = 1
    }

}