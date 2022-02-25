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
    public class WishListRepository: IWishListRepository
    {
        private readonly IConfiguration _config;
        public readonly IMongoDatabase Db;
        public WishListRepository(IOptions<Setting> options, IConfiguration config)
        {
            _config = config;
            var client = new MongoClient(options.Value.Connectionstring);
            Db = client.GetDatabase(options.Value.DatabaseName);
        }
        public IMongoCollection<WishListModel> WishList =>
            Db.GetCollection<WishListModel>("WishList");


        public async Task<WishListModel> Addinwishlist(WishListModel wish)
        {
            try
            {
                var check = await this.WishList.Find(x => x.wishlistID == wish.wishlistID).SingleOrDefaultAsync();
                if (check == null)
                {
                    await this.WishList.InsertOneAsync(wish);
                    return wish;

                }
                return null;

            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }


        public IEnumerable<WishListModel> GetWishlist()
        {
            return WishList.Find(FilterDefinition<WishListModel>.Empty).ToList();
        }


        public async Task<bool> DeleteWishlist(WishListModel del)
        {
            try
            {
                await this.WishList.FindOneAndDeleteAsync(x => x.wishlistID == del.wishlistID);
                return true;

            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
