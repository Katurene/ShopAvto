using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ShopAvto.Data.Models
{
    public class ShopCart
    {
        private readonly AppDBContent appDBContent;

        public ShopCart(AppDBContent appDBContent)
        {
            this.appDBContent = appDBContent;
        }

        public string ShopCartId { get; set; }

        public List<ShopCartItem> shopCartItems { get; set; }  //список всех элементов корзины

        //если есть корзина с товаром то добавить в нее еще один, если нет, то
        //создать корзину и поместить в нее товар

        public static ShopCart GetCart(IServiceProvider services)//для работы с сессиями 
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<AppDBContent>();
            //если идентификатор CartId не сущ то будем его создавать при пом Guid
            string shopCartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            //устан нов сессию и в качестве ключа исп CartId и значение уст shopCartId
            session.SetString("CartId", shopCartId);

            return new ShopCart(context) { ShopCartId = shopCartId };
        }

        //метод позволяющий добавлять товары в корзину
        public void AddToCart(Car car)
        {
            appDBContent.ShopCartItems.Add(new ShopCartItem
            {
                ShopCartId = ShopCartId,
                Car = car,
                ItemPrice = car.Price
            });

            appDBContent.SaveChanges();
        }

        //метод для отображения товаров в корзине
        public List<ShopCartItem> getShopItems()
        {//выбираем только те элементы у которых ид корзины равно ид корзины кот установлена в сессии
            return appDBContent.ShopCartItems.Where(c => c.ShopCartId == ShopCartId).Include(s => s.Car).ToList();
        }
    }
}
