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

       public async Task<LoginResponse> Login(LoginModel login)
        {
            try
            {
                return await this.repository.Login(login);

            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async  Task<bool> Forget(ForgetModel fmodel)
        {
            try
            {
                return await this.repository.Forget(fmodel);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<RegisterModel> Reset(ResetModel reset)
        {
            try
            {
                return await this.repository.Reset(reset);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
