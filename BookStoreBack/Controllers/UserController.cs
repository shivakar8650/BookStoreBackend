using BookStoreManager.Interface;
using BookStoreModel;
using Microsoft.AspNetCore.Authorization;
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


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel login)
        {
            try
            {
                
                var response = await this.manager.Login(login);
                if (response != null)
                {                
                    return this.Ok(new ResponseModel<LoginResponse> { Status = true, Message = "Login Successfully", Data = response });                 
                }
                else
                {                
                    return this.BadRequest(new { Status = false, Message = "Login Unsuccessful" });
                }
            }
            catch (Exception ex)
            {              
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("forget")]
        public async Task<IActionResult> Forget(ForgetModel fmodel)
        {
            try
            {
                
                var resp = await this.manager.Forget(fmodel);
                if (resp == true)
                {                 
                    return this.Ok(new { Status = true, Message = "Link Send Successfully", Data = resp });
                }
                else
                {
                  
                    return this.BadRequest(new { Status = false, Message = "Link not Sent" });
                }
            }
            catch (Exception e)
            {           
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }
        [Authorize]
        [HttpPost]
        [Route("reset")]
        public async Task<IActionResult> Reset([FromBody] ResetModel reset)
        {
            try
            {
            
                var response = await this.manager.Reset(reset);
                if (response != null)
                {
                    return this.Ok(new ResponseModel<RegisterModel> { Status = true, Message = "User Password Reset Successful", Data = response });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "User Password not Reset" });
                }
            }
            catch (Exception e)
            {
              
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }

    }
}
