using BookStoreModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreManager.Interface
{
    public interface IWishListManager
    {
        Task<WishListModel> Addinwishlist(WishListModel wish);
    }
}
