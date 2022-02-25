using BookStoreManager.Interface;
using BookStoreModel;
using BookStoreRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreManager.Services
{
    public class AddressManager : IAddressManager
    {
        private readonly IAddressRepository repo;
        public AddressManager(IAddressRepository repo)
        {
            this.repo = repo;
        }
        public async Task<AddressModel> AddAddress(AddressModel add)
        {
            try
            {
                return await this.repo.AddAddress(add);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<AddressModel> GetallAddress()
        {
            try
            {
                return this.repo.GetallAddress();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<AddressModel> UpdateAddress(AddressModel edit)
        {
            try
            {
                return await this.repo.UpdateAddress(edit);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<AddressModel> GetByAddressType(string addtypeId)
        {
            try
            {
                return await this.repo.GetByAddressType(addtypeId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> DeleteAddress(AddressModel del)
        {
            try
            {
                return await this.repo.DeleteAddress(del);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
