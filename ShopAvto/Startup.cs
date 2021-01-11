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
        public void ConfigureServices(IServiceCollection services)//������ ��� ����������� �������-�������� �������
        {
            services.AddDbContext<AppDBContent>(options => options.UseSqlServer(confString.GetConnectionString("DefaultConnection")));//����� ������ ������ - ���������� Microsoft.EntityFrameworkCore.SqlServer
            services.AddTransient<IAllCars, CarRepository>();//���� �������� ����� ���������� ����������
            services.AddTransient<ICarsCategory, CategoryRepository>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();//��� ������ � ��������

            //����� ��� ������������ ������ �� �������, �� ����� ������ ������ �������
            services.AddScoped(sp => ShopCart.GetCart(sp));//����� mvc

            services.AddMvc(option => option.EnableEndpointRouting = false);//���� ��������� ���� �� ������� �� ����� ������
            //services.AddTransient<IAllCars, MockCars>();//���� ����� ��������� � ����� ��� ������ ���� ��������� <���������, ����� ������� ��� ������>
            //services.AddTransient<ICarsCategory, MockCategory>();

            services.AddMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();//����������� ������
            app.UseStatusCodePages();//��������� ���� �������
            app.UseStaticFiles();//���������� ���� �������� css ������ �����

            app.UseSession();
            //app.UseMvcWithDefaultRoute();//url �� ���������

            //����������� url �� ���������
            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{Id?}");
                //Car-��� ����������� �����, action-����� �����, category- ���� ����� ����� ��������� � CarsController/List(string category), ?-������ ��������
                //defaults-�� ���������
                routes.MapRoute(name: "categoryFilter", template: "Car/{action}/{category?}", defaults: new { Controller = "Car", action = "List" });
            });


            using (var scope = app.ApplicationServices.CreateScope())//���� ��� ���������
            {
                AppDBContent content = scope.ServiceProvider.GetRequiredService<AppDBContent>();//���������� AppDBContent
                DBObjects.Initial(content);//������ � ����� Initial �����-�
            }


        }
    }
}
