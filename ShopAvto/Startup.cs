using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShopAvto.Data;
using ShopAvto.Data.Interfaces;
using ShopAvto.Data.Mocks;
using Microsoft.EntityFrameworkCore;
using ShopAvto.Data.Repository;
using ShopAvto.Data.Models;

namespace ShopAvto
{
    public class Startup
    {
        private IConfigurationRoot confString;

        public Startup(IWebHostEnvironment hostingEnvironment)
        {
            confString = new ConfigurationBuilder().SetBasePath(hostingEnvironment.ContentRootPath).AddJsonFile("dbsettings.json").Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)//служит для регистрации модулей-плагинов проекта
        {
            services.AddDbContext<AppDBContent>(options => options.UseSqlServer(confString.GetConnectionString("DefaultConnection")));//чтобы убрать ошибку - установить Microsoft.EntityFrameworkCore.SqlServer
            services.AddTransient<IAllCars, CarRepository>();//надо поменять класс реализации интерфейса
            services.AddTransient<ICarsCategory, CategoryRepository>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();//для работы с сессиями

            //когда два пользователя зайдут на корзину, то будут выданы разные корзины
            services.AddScoped(sp => ShopCart.GetCart(sp));//перед mvc

            services.AddMvc(option => option.EnableEndpointRouting = false);//надо выставить чтоб не ругался на новой версии
            //services.AddTransient<IAllCars, MockCars>();//позв объед интерфейс и класс кот реализ этот интерфейс <интерфейс, класс который его реализ>
            //services.AddTransient<ICarsCategory, MockCategory>();

            services.AddMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();//отображение ошибок
            app.UseStatusCodePages();//отобразит коды страниц
            app.UseStaticFiles();//отображает разл картинки css статич файлы

            app.UseSession();
            //app.UseMvcWithDefaultRoute();//url по умолчанию

            //собственный url по умолчанию
            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{Id?}");
                //Car-имя контроллера точно, action-любой метод, category- долж точно соотв параметру в CarsController/List(string category), ?-необяз параметр
                //defaults-по умолчанию
                routes.MapRoute(name: "categoryFilter", template: "Car/{action}/{category?}", defaults: new { Controller = "Car", action = "List" });
            });


            using (var scope = app.ApplicationServices.CreateScope())//созд доп окружение
            {
                AppDBContent content = scope.ServiceProvider.GetRequiredService<AppDBContent>();//подключаем AppDBContent
                DBObjects.Initial(content);//перебр в метод Initial перем-ю
            }


        }
    }
}
