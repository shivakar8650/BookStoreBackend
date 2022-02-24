using BookStoreManager.Interface;
using BookStoreModel;
using BookStoreRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreManager.Services
{
    public class CartManager : ICartManager
    {
        private readonly ICartRepository repo;
        public CartManager(ICartRepository repo)
        {
            this.repo = repo;
        }
        public async Task<CartModel> AddtoCart(CartModel cart)
        {
            try
            {
                return await this.repo.AddtoCart(cart);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<CartModel> GetCart()
        {
            try
            {
                return this.repo.GetCart();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<CartModel> updateQuantity(CartModel qty)
        {
            try
            {
                return await this.repo.updateQuantity( qty);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> deleteCart(CartModel del)
        {
            try
            {
                return await this.repo.deleteCart(del);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
        
}
