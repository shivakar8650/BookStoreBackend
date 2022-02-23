using BookStoreModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreManager.Interface
{
    public interface IUserManager
    {
        Task<RegisterModel> Register(RegisterModel register);
        Task<LoginResponse> Login(LoginModel login);
        Task<bool> Forget(ForgetModel email);

        Task<RegisterModel> Reset(ResetModel reset);
    }
}
