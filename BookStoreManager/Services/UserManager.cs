using BookStoreManager.Interface;
using BookStoreModel;
using BookStoreRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreManager.Services
{
    public class UserManager : IUserManager
    {
        public readonly IUserRepository repository;
        public UserManager(IUserRepository repo)
        {
            this.repository = repo;
        }

        public async Task<RegisterModel> Register(RegisterModel register)
        {
            try
            {
                return await this.repository.Register(register);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
