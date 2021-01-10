using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopAvto.Data.Interfaces;
using ShopAvto.ViewModels;

namespace ShopAvto.Controllers
{
    public class HomeController : Controller
    {
        private IAllCars CarRepository;
      
        public HomeController(IAllCars carRepository)
        {
            CarRepository = carRepository;          
        }

        public ViewResult Index()
        {
            var homeCars = new HomeViewModel
            {
                favCars = CarRepository.getFavCars
            };
            return View(homeCars);//все товары кот долж отобр на гл стр
        }
    }
}
