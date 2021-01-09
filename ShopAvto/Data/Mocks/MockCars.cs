using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopAvto.Data.Interfaces;
using ShopAvto.Data.Models;

namespace ShopAvto.Data.Mocks
{
    public class MockCars : IAllCars
    {
        private readonly ICarsCategory carsCategory = new MockCategory();//при помощи переменной можно указ к как кат принадл объект

        public IEnumerable<Car> cars
        {
            get
            {
                return new List<Car>
                {
                    new Car {Name = "Tesla", ShortDescription = "", LongDescription = "", Img = "/img/tesla.jpg",
                    Price = 40000, IsFavorite = true, Available = true, Category = carsCategory.allCategories.First() },//allCategories.First указ что тесла электро
                new Car {Name = "Ford", ShortDescription = "", LongDescription = "", Img = "/img/ford.jpg",
                    Price = 4000, IsFavorite = false, Available = true, Category = carsCategory.allCategories.Last() },
                new Car {Name = "BMW", ShortDescription = "", LongDescription = "", Img = "/img/bmw.jpg",
                    Price = 20000, IsFavorite = true, Available = true, Category = carsCategory.allCategories.Last() },
                new Car {Name = "Mersedes", ShortDescription = "", LongDescription = "", Img = "/img/mercedes.jpg",
                    Price = 31000, IsFavorite = false, Available = false, Category = carsCategory.allCategories.Last() },
                new Car {Name = "Nissan", ShortDescription = "", LongDescription = "", Img = "/img/nissan.jpg",
                    Price = 15000, IsFavorite = true, Available = true, Category = carsCategory.allCategories.First() }
                };
            }
        }
        public IEnumerable<Car> getFavCars { get; set; }

        public Car getObjectCar(int carId)
        {
            throw new NotImplementedException();
        }
    }
}
