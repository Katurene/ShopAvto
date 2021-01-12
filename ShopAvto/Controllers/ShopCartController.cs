using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopAvto.Data.Interfaces;
using ShopAvto.Data.Models;
using ShopAvto.Data.Repository;
using ShopAvto.ViewModels;

namespace ShopAvto.Controllers
{
    public class ShopCartController : Controller
    {
        private IAllCars CarRepository;
        private readonly ShopCart ShopCart;


        public ShopCartController(IAllCars carRepository, ShopCart shopCart)
        {
            CarRepository = carRepository;
            ShopCart = shopCart;
        }

        public ViewResult Index()
        {
            var items = ShopCart.getShopItems();
            ShopCart.shopCartItems = items;

            var obj = new ShopCartViewModel
            {
                ShopCart = ShopCart
            };

            return View(obj);
        }

        //переадресация на др строницу и доб тов в корз
        public RedirectToActionResult AddToCart(int id) //int id == asp-route-id(id-название )
        {
            //перем-я выб нужный товар из списка всех товаров по id
            var item = CarRepository.cars.FirstOrDefault(i => i.Id == id);

            if(item!=null)
            {
                ShopCart.AddToCart(item);
            }
            return RedirectToAction("Index");//переадресация
        }
    }
}
