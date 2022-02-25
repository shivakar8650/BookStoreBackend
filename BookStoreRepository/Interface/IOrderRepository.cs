using BookStoreModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRepository.Interface
{
    public interface IOrderRepository
    {
        Task<OrderModel> AddOrder(OrderModel wish);

        IEnumerable<OrderModel> GetOrder();
        Task<bool> CancleOrder(OrderModel del);
    }
}
