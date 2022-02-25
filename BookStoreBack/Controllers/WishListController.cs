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

        [HttpGet]
        public IActionResult GetallWishlist()
        {
            try
            {
                IEnumerable<WishListModel> response = this.WM.GetWishlist();
                if (response != null)
                {
                    return this.Ok(new { Status = true, Message = "Wishlist Retrived Successfully", Data = response });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Wishlist is Empty" });
                }

            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteWishlist([FromBody] WishListModel del)
        {
            try
            {
                var check = await this.WM.DeleteWishlist(del);
                if (check != false)
                {
                    return this.Ok(new ResponseModel<WishListModel> { Status = true, Message = "Book Removed from Wishlist" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Book not Removed from Wishlist" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }
    }
}
