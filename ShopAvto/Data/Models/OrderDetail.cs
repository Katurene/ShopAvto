using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopAvto.Data.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int CarId { get; set; }
        public uint Price { get; set; }
        public virtual Car Car { get; set; }//объект с кот мы работаем
        public virtual Order Order { get; set; }//заказ с кот мы работаем
    }
}
