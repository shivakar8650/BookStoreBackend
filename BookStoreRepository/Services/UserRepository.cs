using BookStoreModel;
using BookStoreRepository.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;

using System.Text;
using System.Threading.Tasks;

namespace BookStoreRepository.Services
{
    public class UserRepository : IUserRepository
    {

        public readonly IMongoDatabase Db;
        public UserRepository(IOptions<Setting> options)
        {
            var client = new MongoClient(options.Value.Connectionstring);
            Db = client.GetDatabase(options.Value.DatabaseName);
        }
        public IMongoCollection<RegisterModel> User =>
            Db.GetCollection<RegisterModel>("User");

        public async Task<RegisterModel> Register(RegisterModel register)
        {
            register.password = EncodePasswordToBase64(register.password);
            try
            {
                var check = await this.User.AsQueryable().Where(x => x.emailID == register.emailID).SingleOrDefaultAsync();
                if (check == null)
                {
                    await this.User.InsertOneAsync(register);
                    return register;

                }
                return null;

            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
          }

        public static string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
    }
}
