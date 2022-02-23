using BookStoreModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreManager.Interface
{
   public  interface IBookManager
    {
        Task<BooksModel> AddBook(BooksModel addbook);
        Task<BooksModel> Update(BooksModel book);
        BooksModel FindBook(string id);

        IEnumerable<BooksModel> GetAllBooks();

        Task<BooksModel> Addimage(string bookId, IFormFile img);

        Task<bool> DeleteBook(string bookId);
    }
}
