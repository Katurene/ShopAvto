using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShopAvto.Data.Interfaces;
using ShopAvto.Data.Mocks;

namespace ShopAvto
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)//служит для регистрации модулей-плагинов проекта
        {
            services.AddMvc(option => option.EnableEndpointRouting = false);//надо выставить чтоб не ругался на новой версии
            services.AddTransient<IAllCars, MockCars>();//позв объед интерфейс и класс кот реализ этот интерфейс <интерфейс, класс который его реализ>
            services.AddTransient<ICarsCategory, MockCategory>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();//отображение ошибок
            app.UseStatusCodePages();//отобразит коды страниц
            app.UseStaticFiles();//отображает разл картинки css статич файлы
            app.UseMvcWithDefaultRoute();//url по умолчанию
        }
    }
}
