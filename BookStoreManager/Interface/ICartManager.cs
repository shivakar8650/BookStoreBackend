using BookStoreModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreManager.Interface
{
    public interface ICartManager
    {

        Task<CartModel> AddtoCart(CartModel cart);
    }
}
