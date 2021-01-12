using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopAvto.Data.Interfaces;
using ShopAvto.Data.Models;

namespace ShopAvto.Data.Repository
{
    public class OrdersRepository : IAllOrders
    {
        private readonly AppDBContent appDBContent;
        private readonly ShopCart shopCart;

        public OrdersRepository(AppDBContent appDBContent, ShopCart shopCart)
        {
            this.appDBContent = appDBContent;
            this.shopCart = shopCart;
        }

        public void CreateOrder(Order order)
        {
            order.OrderTime = DateTime.Now;//время заказа текущее
            appDBContent.Orders.Add(order);//помещаем заказ в таблицу
            appDBContent.SaveChanges();//важно! сначала сохраняем!

            var items = shopCart.shopCartItems;//получ все товары кот приобр польз-ль
        
            foreach(var el in items)
            {
                var orderDetail = new OrderDetail()//создаем объект
                {
                    CarId = el.Car.Id,
                    OrderId = order.Id,
                    Price = el.Car.Price
                };
                appDBContent.OrderDetails.Add(orderDetail);// добавляем объект в БД
            }          
        }
    }
}
