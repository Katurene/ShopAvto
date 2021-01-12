using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ShopAvto.Data.Models
{
    public class Order
    {
        [BindNever]
        public int Id { get; set; }

        [Display(Name="Введите имя")]
        [StringLength(15)] //проверка длины строки - не больше 5
        [Required(ErrorMessage ="Длина имени не более 15 символов")] //сообщение об ошибке
        public string Name { get; set; }

        [Display(Name = "Введите фамилию")]
        [StringLength(15)] 
        [Required(ErrorMessage = "Длина фамилии не более 15 символов")]
        public string Surname { get; set; }

        [Display(Name = "Введите адрес")]
        [StringLength(10)]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Длина адреса не более 10 символов")]
        public string Address { get; set; }

        [Display(Name = "Введите номер телефона")]
        [StringLength(7)]
        [Required(ErrorMessage = "Длина номера телефона не более 7 символов")]
        public string Phone { get; set; }

        [Display(Name = "Введите Email")]
        [DataType(DataType.EmailAddress)]//задать тип вводимых данных
        [StringLength(17)]
        [Required(ErrorMessage = "Длина email не более 17 символов")]
        public string Email { get; set; }

        [BindNever]
        [ScaffoldColumn(false)]//чтобы поле не отображалось в исходном коде
        public DateTime OrderTime { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }//список всех приобрет товаров
    }
}
