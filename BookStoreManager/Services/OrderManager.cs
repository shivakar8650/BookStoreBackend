using BookStoreManager.Interface;
using BookStoreModel;
using BookStoreRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreManager.Services
{
    public class OrderManager : IOrderManager
    {
        private readonly IOrderRepository repo;
        public OrderManager(IOrderRepository repo)
        {
            this.repo = repo;
        }
        public async Task<OrderModel> AddOrder(OrderModel order)
        {
            try
            {
                return await this.repo.AddOrder(order);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<OrderModel> GetOrder()
        {
            try
            {
                return this.repo.GetOrder();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> CancleOrder(OrderModel del)
        {
            try
            {
                return await this.repo.CancleOrder(del);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
