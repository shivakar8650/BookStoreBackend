using BookStoreModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRepository.Interface
{
    public interface IAddressRepository
    {
        Task<AddressModel> AddAddress(AddressModel add);
        IEnumerable<AddressModel> GetallAddress();
        Task<AddressModel> UpdateAddress(AddressModel edit);

        Task<AddressModel> GetByAddressType(string addtypeId);

        Task<bool> DeleteAddress(AddressModel del);
    }
}
