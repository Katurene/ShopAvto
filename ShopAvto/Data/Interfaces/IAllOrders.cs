using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopAvto.Data.Models;

namespace ShopAvto.Data.Interfaces
{
    public interface IAllOrders
    {
        void CreateOrder(Order order);
    }
}
