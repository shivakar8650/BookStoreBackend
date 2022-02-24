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
    public class CartController : ControllerBase
    {

        private readonly ICartManager CM;

        public CartController(ICartManager manager)
        {
            this.CM = manager;
        }

        [HttpPost]
        [Route("addtocart")]
        public async Task<IActionResult> AddtoCart([FromBody] CartModel cart)
        {
            try
            {
                var response = await this.CM.AddtoCart(cart);
                if (response != null)
                {
                    return this.Ok(new ResponseModel<CartModel> { Status = true, Message = "Book Added to Cart", Data = response });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Book Not Added to Cart" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }
    }
}
