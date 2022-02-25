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
    public class AddressRepository : IAddressRepository
    {
        private readonly IConfiguration _config;
        public readonly IMongoDatabase Db;
        public AddressRepository(IOptions<Setting> options, IConfiguration config)
        {
            _config = config;
            var client = new MongoClient(options.Value.Connectionstring);
            Db = client.GetDatabase(options.Value.DatabaseName);
        }
        public IMongoCollection<AddressModel> Address =>
            Db.GetCollection<AddressModel>("Address");

        public async Task<AddressModel> AddAddress(AddressModel add)
        {
            try
            {
                var check = await this.Address.Find(x => x.addressID == add.addressID).SingleOrDefaultAsync();
                if (check == null)
                {
                    await this.Address.InsertOneAsync(add);
                    return add;

                }
                return null;

            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }


        public IEnumerable<AddressModel> GetallAddress()
        {
            return this.Address.Find(FilterDefinition<AddressModel>.Empty).ToList();
        }

        public async Task<AddressModel> UpdateAddress(AddressModel update)
        {
            try
            {
                var valid = await this.Address.Find(x => x.addressID == update.addressID).FirstOrDefaultAsync();
                if (valid != null)
                {
                    await this.Address.UpdateOneAsync(x => x.addressID == update.addressID,
                        Builders<AddressModel>.Update.Set(x => x.fullAddress, update.fullAddress)
                        .Set(x => x.city, update.city)
                        .Set(x => x.state, update.state)
                        .Set(x => x.pinCode, update.pinCode));
                    var response = await this.Address.Find(x => x.addressID == update.addressID).FirstOrDefaultAsync();
                    return response;

                }
                return null;

            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);

            }
        }

        public async Task<AddressModel> GetByAddressType(string addtypeId)
        {
            return await this.Address.Find(x => x.addTypeID == addtypeId).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteAddress(AddressModel del)
        {
            try
            {
                var check = await this.Address.FindOneAndDeleteAsync(x => x.addressID == del.addressID);
                return true;

            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
