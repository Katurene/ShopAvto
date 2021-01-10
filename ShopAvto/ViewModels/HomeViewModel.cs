using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopAvto.Data.Models;

namespace ShopAvto.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Car> favCars { get; set; }
    }
}
