using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopAvto.Data.Interfaces;
using ShopAvto.Data.Models;
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

        [Route("Cars/List")]   //здесь указ тот url, при переходе на кот будет срабат этот метод
        [Route("Cars/List/{category}")]
        public ViewResult List(string category)
        {
            //ViewBag.Category = "Some New";//для передачи данных внутрь html-шаблона (Category = "Some New")-любое название и его значение-любое
            //не рекомендуется исп тк для каждого значения новый viewbag
            //var cars = allCars.cars;//через интерфейс обращ к методу IEnumerable<Car> cars
            //return View(cars);

            string _category = category;
            IEnumerable<Car> cars = null;//присвоить значение чтоб внизу без ошиб
            string curCategory = "";
            if (string.IsNullOrEmpty(category))
            {
                cars = allCars.cars.OrderBy(i => i.Id);//сортируем по id
            }
            else
            {//если строка равна электро + не учитываем регистры
                if (string.Equals("electro", category, StringComparison.OrdinalIgnoreCase))
                {
                    cars = allCars.cars.Where(i => i.Category.CategoryName.Equals("Электромобили")).OrderBy(i => i.Id);
                    curCategory = "Электромобили";//присваиваем название для категории
                }
                else if (string.Equals("fuel", category, StringComparison.OrdinalIgnoreCase))
                {
                    cars = allCars.cars.Where(i => i.Category.CategoryName.Equals("Классические автомобили")).OrderBy(i => i.Id);
                    curCategory = "Классические автомобили";//присваиваем название для категории
                }

                
            }

            var carObject = new CarsListViewModel
            {
                allCars = cars,
                CurrentCategory = curCategory
            };

            ViewBag.Title = "Страница с автомобилями";
            //CarsListViewModel obj = new CarsListViewModel();
            //obj.allCars = allCars.cars;
            //obj.CurrentCategory = "Автомобили";

            return View(carObject);
        }
    }
}
