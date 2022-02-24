using BookStoreManager.Interface;
using BookStoreModel;
using BookStoreRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreManager.Services
{
    public class WishListManager: IWishListManager
    {
        private readonly IWishListRepository repo;
        public WishListManager(IWishListRepository repo)
        {
            this.repo = repo;
        }
        public async Task<WishListModel> Addinwishlist(WishListModel wish)
        {
            try
            {
                return await this.repo.Addinwishlist(wish);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
