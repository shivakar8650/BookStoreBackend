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
    public class CartRepository : ICartRepository
    {
        private readonly IConfiguration _config;
        public readonly IMongoDatabase Db;
        public CartRepository(IOptions<Setting> options, IConfiguration config)
        {
            _config = config;
            var client = new MongoClient(options.Value.Connectionstring);
            Db = client.GetDatabase(options.Value.DatabaseName);
        }
        public IMongoCollection<CartModel> Cart =>
            Db.GetCollection<CartModel>("Cart");


        public async Task<CartModel> AddtoCart(CartModel cart)
        {
            try
            {
                var valid = await this.Cart.Find(x => x.cartID == cart.cartID).SingleOrDefaultAsync();
                if (valid == null)
                {
                    await this.Cart.InsertOneAsync(cart);
                    return cart;

                }
                return null;

            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<CartModel> GetCart()
        {
            return Cart.Find(FilterDefinition<CartModel>.Empty).ToList();

        }

        public async Task<CartModel> updateQuantity(CartModel qty)
        {    
                try
                {
                    var valid = await this.Cart.Find(x => x.cartID == qty.cartID).FirstOrDefaultAsync();
                    if (valid != null)
                    {
                        await this.Cart.UpdateOneAsync(x => x.cartID == qty.cartID,
                            Builders<CartModel>.Update.Set(x => x.quantity, qty.quantity));
                    var response = await this.Cart.Find(x => x.cartID == qty.cartID).FirstOrDefaultAsync();
                    return response;

                    }
                    return null;

                }
                catch (ArgumentNullException e)
                {
                    throw new Exception(e.Message);
                }

        }

        public async Task<bool> deleteCart(CartModel del)
        {
            try
            {
                await this.Cart.FindOneAndDeleteAsync(x => x.cartID == del.cartID);
                return true;

            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }

    }


}
