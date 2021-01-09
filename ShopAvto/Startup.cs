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
            services.AddMvc(option => option.EnableEndpointRouting = false);//���� ��������� ���� �� ������� �� ����� ������
            //services.AddTransient<IAllCars, MockCars>();//���� ����� ��������� � ����� ��� ������ ���� ��������� <���������, ����� ������� ��� ������>
            //services.AddTransient<ICarsCategory, MockCategory>();

            services.AddTransient<IAllCars, CarRepository>();//���� �������� ����� ���������� ����������
            services.AddTransient<ICarsCategory, CategoryRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();//����������� ������
            app.UseStatusCodePages();//��������� ���� �������
            app.UseStaticFiles();//���������� ���� �������� css ������ �����
            app.UseMvcWithDefaultRoute();//url �� ���������

             

            using (var scope = app.ApplicationServices.CreateScope())//���� ��� ���������
            {
                AppDBContent content = scope.ServiceProvider.GetRequiredService<AppDBContent>();//���������� AppDBContent
                DBObjects.Initial(content);//������ � ����� Initial �����-�
            }

            
        }
    }
}
