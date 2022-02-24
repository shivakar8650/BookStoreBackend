using BookStoreManager.Interface;
using BookStoreModel;
using BookStoreRepository.Interface;
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
    public class WishListController : ControllerBase
    {
        private readonly IWishListManager WM;

        public WishListController(IWishListManager manager)
        {
            this.WM = manager;
        }

        [HttpPost] 
        public async Task<IActionResult> Addinwishlist([FromBody] WishListModel book)
        {
            try
            {
                var response = await this.WM.Addinwishlist(book);
                if (response != null)
                {
                    return this.Ok(new ResponseModel<WishListModel> { Status = true, Message = "Added to wishlist succesfully!", Data = response });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Book Not Added to Wishlist" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }
    }
}
