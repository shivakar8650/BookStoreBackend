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
    }
        
}
