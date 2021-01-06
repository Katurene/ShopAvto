using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopAvto.Data.Models;

namespace ShopAvto.ViewModels
{
    public class CarsListViewModel
    {
        public IEnumerable<Car> allCars { get; set; }//получает все товары
        public string CurrentCategory { get; set; }//текущая категория с кот работаем
    }
}
