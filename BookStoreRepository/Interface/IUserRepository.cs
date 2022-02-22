using BookStoreModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRepository.Interface
{
    public interface IUserRepository
    {
        Task<RegisterModel> Register(RegisterModel register);
    }
}
