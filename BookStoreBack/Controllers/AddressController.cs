using BookStoreManager.Interface;
using BookStoreModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {

        private readonly IAddressManager AM;

        public AddressController(IAddressManager manager)
        {
            this.AM = manager;
        }

        [HttpPost]
        public async Task<IActionResult> AddAddress([FromBody] AddressModel add)
        {
            try
            {
                var response = await this.AM.AddAddress(add);
                if (response != null)
                {
                    return this.Ok(new ResponseModel<AddressModel> { Status = true, Message = "Address Added Successfully", Data = response });
                }
                else
                {
                    return this.Ok(new { Status = false, Message = "Address not Added" });
                }
            }
            catch (Exception e)
            {
                return this.Ok(new { Status = false, Message = e.Message });
            }
        }


        [HttpGet]
        [Route("alladdress")]
        public IActionResult GetallAddress()
        {
            try
            {
                IEnumerable<AddressModel> response = this.AM.GetallAddress();
                if (response != null)
                {
                    return this.Ok(new { Status = true, Message = "Address Retrived Successfully", Data = response});
                }
                else
                {
                    return this.Ok(new { Status = false, Message = "Address not Retrived" });
                }
            }
            catch (Exception e)
            {
                return this.Ok(new { Status = false, Message = e.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAddress([FromBody] AddressModel edit)
        {
            try
            {
                var response = await this.AM.AddAddress(edit);
                if (response != null)
                {
                    return this.Ok(new ResponseModel<AddressModel> { Status = true, Message = "Address Updated Successfully", Data = response });
                }
                else
                {
                    return this.Ok(new { Status = false, Message = "Address not Updated" });
                }
            }
            catch (Exception e)
            {
                return this.Ok(new { Status = false, Message = e.Message });
            }
        }


        [HttpGet]
        [Route("addresstype")]
        public async Task<IActionResult> GetByAddressType(string addtypeId)
        {
            try
            {
                var response = await this.AM.GetByAddressType(addtypeId);
                if (response != null)
                {
                    return this.Ok(new ResponseModel<AddressModel> { Status = true, Message = "Address Retrived Successfully", Data = response });
                }
                else
                {
                    return this.Ok(new { Status = false, Message = "Address not Retrived" });
                }
            }
            catch (Exception e)
            {
                return this.Ok(new { Status = false, Message = e.Message });
            }
        }

        [HttpDelete]
        [Route("deleteaddress")]
        public async Task<IActionResult> DeleteAddress([FromBody] AddressModel del)
        {
            try
            {
                var check = await this.AM.DeleteAddress(del);
                if (check == true)
                {
                    return this.Ok(new ResponseModel<AddressModel> { Status = true, Message = "Address Deleted Successfully" });
                }
                else
                {
                    return this.Ok(new { Status = false, Message = "Address not Deleted" });
                }
            }
            catch (Exception e)
            {
                return this.Ok(new { Status = false, Message = e.Message });
            }
        }
    }
}
