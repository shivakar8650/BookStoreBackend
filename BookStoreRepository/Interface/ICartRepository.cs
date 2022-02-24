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

        IEnumerable<CartModel> GetCart();

        Task<CartModel> updateQuantity(CartModel qty);

        Task<bool> deleteCart(CartModel del);
    }
}
