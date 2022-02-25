using BookStoreModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreManager.Interface
{
    public interface IOrderManager
    {
        Task<OrderModel> AddOrder(OrderModel order);
        IEnumerable<OrderModel> GetOrder();

        Task<bool> CancleOrder(OrderModel del);

    }
}
