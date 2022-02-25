using BookStoreModel;
using BookStoreRepository.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRepository.Services
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IConfiguration _config;
        public readonly IMongoDatabase Db;
        public OrderRepository(IOptions<Setting> options, IConfiguration config)
        {
            _config = config;
            var client = new MongoClient(options.Value.Connectionstring);
            Db = client.GetDatabase(options.Value.DatabaseName);
        }
        public IMongoCollection<OrderModel> Order =>
            Db.GetCollection<OrderModel>("Order");

        public async Task<OrderModel> AddOrder(OrderModel order)
        {
            try
            {
                var valid = await this.Order.Find(x => x.orderID == order.orderID).SingleOrDefaultAsync();
                if (valid == null)
                {
                    await this.Order.InsertOneAsync(order);
                    return order;

                }
                return null;

            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<OrderModel> GetOrder()
        {
            
            return Order.Find(FilterDefinition<OrderModel>.Empty).ToList();
        }

        public async Task<bool> CancleOrder(OrderModel del)
        {
            try
            {
                await this.Order.FindOneAndDeleteAsync(x => x.orderID == del.orderID);
                return true;
            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
