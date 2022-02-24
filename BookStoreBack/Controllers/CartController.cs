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


        [HttpGet] 
        public IActionResult GetCart()
        {
            try
            {
                IEnumerable<CartModel> check = this.CM.GetCart();
                if (check != null)
                {
                    return this.Ok(new { Status = true, Message = "Cart Retrived Successfully", Data = check });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Cart is Empty" });
                }

            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }

        [HttpPut]
        [Route("quantity")]
        public async Task<IActionResult> updateQuantity([FromBody] CartModel edit)
        {
            try
            {
                var response = await this.CM.updateQuantity(edit);
                if (response != null)
                {
                    return this.Ok(new ResponseModel<CartModel> { Status = true, Message = "Book Quantity Updated", Data = response });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Cannot Update Quantity" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCart([FromBody] CartModel del)
        {
            try
            {
                var response = await this.CM.deleteCart(del);
                if (response != false)
                {
                    return this.Ok(new ResponseModel<CartModel> { Status = true, Message = "Book Removed from Cart" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Book not Removed from Cart" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }

    }
}
