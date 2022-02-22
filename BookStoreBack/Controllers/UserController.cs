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
    public class UserController : ControllerBase
    {

        private readonly IUserManager manager;
        public UserController(IUserManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel register)
        {
            try
            {
             
                var response = await this.manager.Register(register);
                if (response != null)
                {
                   
                    return this.Ok(new ResponseModel<RegisterModel> { Status = true, Message = "User Registered Successfully!", Data = response });
                }
                else
                {
              
                    return this.BadRequest(new { Status = false, Message = "User not Registered..." });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }
    }
}
