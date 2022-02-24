using BookStoreModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRepository.Interface
{
    public interface ICartRepository
    {
        Task<CartModel> AddtoCart(CartModel Cart);
    }
}
