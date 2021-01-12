using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopAvto.Data.Interfaces;
using ShopAvto.Data.Models;

namespace ShopAvto.Controllers
{
    public class OrderController : Controller
    {
        private readonly IAllOrders allOrders;
        private readonly ShopCart shopCart;

        public OrderController(IAllOrders allOrders, ShopCart shopCart)
        {
            this.allOrders = allOrders;
            this.shopCart = shopCart;
        }

        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]//отправка post --при завершении заказа вызовется метод
        public IActionResult Checkout(Order order)
        {
            shopCart.shopCartItems = shopCart.getShopItems();
            if(shopCart.shopCartItems.Count==0)
            {
                ModelState.AddModelError("", "У вас должны быть товары!");//указ ключ(здесь пустой) и сообщение 
            }

            if(ModelState.IsValid)
            {
                allOrders.CreateOrder(order);
                return RedirectToAction("Complete");
            }

            return View(order);//форма неверная но данные в ней сохранятся после перезагрузки страницы
        }

        public IActionResult Complete()
        {
            ViewBag.Message = "Заказ успешно обработан";
            return View();
        }
    }
}
