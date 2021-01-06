using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopAvto.Data.Interfaces;
using ShopAvto.ViewModels;

namespace ShopAvto.Controllers
{
    public class CarsController : Controller
    {
        private readonly IAllCars allCars;
        private readonly ICarsCategory carsCategory;

        public CarsController(IAllCars iallCars, ICarsCategory icarsCategory)
        {
            allCars = iallCars;
            carsCategory = icarsCategory;
        }

        public ViewResult List()
        {
            //ViewBag.Category = "Some New";//для передачи данных внутрь html-шаблона (Category = "Some New")-любое название и его значение-любое
            //не рекомендуется исп тк для каждого значения новый viewbag
            //var cars = allCars.cars;//через интерфейс обращ к методу IEnumerable<Car> cars
            //return View(cars);
            ViewBag.Title = "Страница с автомобилями";
            CarsListViewModel obj = new CarsListViewModel();
            obj.allCars = allCars.cars;
            obj.CurrentCategory = "Автомобили";
            return View(obj);
        }
    }
}
