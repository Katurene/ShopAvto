using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ShopAvto.Data.Models;

namespace ShopAvto.Data
{
    public class DBObjects   //все статик чтобы обращ из др классов
    {
        public static void Initial(AppDBContent content)//доб объекты в БД
        {
          

            //доб все категории в бд если их там нет
            if (!content.Categories.Any())//получаем все категории и если они пустые то добавляем их
                content.Categories.AddRange(Categories.Select(c => c.Value));

            if (!content.Cars.Any())  //если нет объектов товаров то создаем их
            {
                content.AddRange(
                      new Car
                      {
                          Name = "Tesla",
                          ShortDescription = "",
                          LongDescription = "",
                          Img = "/img/tesla.jpg",
                          Price = 40000,
                          IsFavorite = true,
                          Available = true,
                          Category = Categories["Электромобили"]//обращаемся через метод Categories к ключу нужного элемента
                      },
                new Car
                {
                    Name = "Ford",
                    ShortDescription = "",
                    LongDescription = "",
                    Img = "/img/ford.jpg",
                    Price = 4000,
                    IsFavorite = false,
                    Available = true,
                    Category = Categories["Классические автомобили"]
                },
                new Car
                {
                    Name = "BMW",
                    ShortDescription = "",
                    LongDescription = "",
                    Img = "/img/bmw.jpg",
                    Price = 20000,
                    IsFavorite = true,
                    Available = true,
                    Category = Categories["Классические автомобили"]
                },
                new Car
                {
                    Name = "Mersedes",
                    ShortDescription = "",
                    LongDescription = "",
                    Img = "/img/mercedes.jpg",
                    Price = 31000,
                    IsFavorite = false,
                    Available = false,
                    Category = Categories["Классические автомобили"]
                },
                new Car
                {
                    Name = "Nissan",
                    ShortDescription = "",
                    LongDescription = "",
                    Img = "/img/nissan.jpg",
                    Price = 15000,
                    IsFavorite = true,
                    Available = true,
                    Category = Categories["Электромобили"]
                }
                    );
            }
            
            content.SaveChanges(); //сохраняем изменения

        }

        private static Dictionary<string, Category> category;

        public static Dictionary<string, Category> Categories
        {
            get
            {
                if (category == null)//если список пустой то помещаем в него нужные категории
                {
                    var list = new Category[]
                    {
                        new Category {CategoryName = "Электромобили", Description = "Современный вид транспорта"},
                    new Category {CategoryName = "Классические автомобили", Description = "Машины с двигателем внутреннего сгорания"}
                    };

                    category = new Dictionary<string, Category>();//выделяем память под переменную
                    foreach (Category element in list)
                        category.Add(element.CategoryName, element);//добавляем нов объект
                }

                return category;//возвр список если объекты в нем есть
            }
        }
    }
}
