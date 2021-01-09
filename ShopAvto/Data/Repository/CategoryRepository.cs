using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopAvto.Data.Interfaces;
using ShopAvto.Data.Models;

namespace ShopAvto.Data.Repository
{
    public class CategoryRepository : ICarsCategory
    {
        private readonly AppDBContent appDBContent;

        public CategoryRepository(AppDBContent appDBContent)
        {
            this.appDBContent = appDBContent;
        }

        public IEnumerable<Category> allCategories => appDBContent.Categories;//получаем все категории
    }
}
