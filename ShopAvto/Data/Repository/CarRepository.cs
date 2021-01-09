using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopAvto.Data.Interfaces;
using ShopAvto.Data.Models;

namespace ShopAvto.Data.Repository
{
    public class CarRepository : IAllCars
    {
        private readonly AppDBContent appDBContent;

        public CarRepository(AppDBContent appDBContent)
        {
            this.appDBContent = appDBContent;
        }

        public IEnumerable<Car> cars => appDBContent.Cars.Include(c => c.Category);//здесь получаем все объекты

        public IEnumerable<Car> getFavCars => appDBContent.Cars.Where(p => p.IsFavorite).Include(c => c.Category);//все объекты где IsFavorite=true 

        public Car getObjectCar(int carId) => appDBContent.Cars.FirstOrDefault(p => p.Id == carId);//один объект где Id == carId

    }
}
