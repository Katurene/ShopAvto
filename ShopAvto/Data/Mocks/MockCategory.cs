using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopAvto.Data.Interfaces;
using ShopAvto.Data.Models;

namespace ShopAvto.Data.Mocks
{
    public class MockCategory : ICarsCategory
    {
        public IEnumerable<Category> allCategories
        {
            get
            {
                return new List<Category>
                {
                    new Category {CategoryName = "Электромобили", Description = "Современный вид транспорта"},
                    new Category {CategoryName = "Классические автомобили", Description = "Машины с двигателем внутреннего сгорания"}
                };
            }
        }
    }
}
