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
    public class BooksController : ControllerBase
    {
        private readonly IBookManager BM;

        public BooksController(IBookManager manager)
        {
            this.BM = manager;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddBook( BooksModel book)
        {
            try
            {
                var ifExists = await this.BM.AddBook(book);
                if (ifExists != null)
                {
                    return this.Ok(new ResponseModel<BooksModel> { Status = true, Message = "Book Added Successfully", Data = ifExists });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Book not Added" });
                }

            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateBook( BooksModel book)
        {
            try
            {
                var ifExists = await this.BM.Update(book);
                if (ifExists != null)
                {
                    return this.Ok(new ResponseModel<BooksModel> { Status = true, Message = "Book Updated Successfully!", Data = ifExists });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Book not Updated" });
                }

            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }


        [HttpGet]
        [Route("byID")]
        public IActionResult GetByBookId(string id)
        {
            try
            {
                var response = this.BM.FindBook(id);
                if (response != null)
                {
                    return this.Ok(new { Status = true, Message = "Book Found!", Data = response });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Book not Found" });
                }

            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }

        [HttpGet]
        [Route("getallbooks")]
        public IActionResult GetAllBook()
        {
            try
            {
                IEnumerable<BooksModel> ifExists = this.BM.GetAllBooks();
                if (ifExists != null)
                {
                    return this.Ok(new { Status = true, Message = "Books Retrived Successfully", Data = ifExists });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Books not Found" });
                }

            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }


        [HttpPut]
        [Route("image")]
        public async Task<IActionResult> BookImg(string bookId, IFormFile image)
        {
            try
            {
                var response = await this.BM.Addimage(bookId, image);
                if (response != null)
                {
                    return this.Ok(new ResponseModel<BooksModel> { Status = true, Message = "Image Uploaded Successfully", Data = response });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Image not Uploaded" });
                }

            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }

        [HttpDelete]
      /*  [Route("deletebook")]*/
        public async Task<IActionResult> DeleteBook(string bookId)
        {
            try
            {
                var response = await this.BM.DeleteBook(bookId);
                if (response == true)
                {
                    return this.Ok(new { Status = true, Message = "Book Deleted Successfully", Data = response });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Book not Deleted" });
                }

            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }
    }
}
