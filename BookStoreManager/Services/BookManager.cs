using BookStoreManager.Interface;
using BookStoreModel;
using BookStoreRepository.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreManager.Services
{
    public class BookManager : IBookManager
    {
        private readonly IBookRepository repo;
        public BookManager(IBookRepository repo)
        {
            this.repo = repo;
        }
        public async Task<BooksModel> AddBook(BooksModel addbook)
        {
            try
            {
                return await this.repo.AddBook(addbook);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public async Task<BooksModel> Update(BooksModel book)
        {
            try
            {
                return await this.repo.Update(book);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public BooksModel FindBook(string id)
        {
            try
            {
                return this.repo.FindBook(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<BooksModel> GetAllBooks()
        {
            try
            {
                return this.repo.GetAllBooks();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public async Task<BooksModel> Addimage(string bookId, IFormFile img)
        {
            try
            {
                return await this.repo.Addimage(bookId, img);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> DeleteBook(string bookId)
        {
            try
            {
                return await this.repo.DeleteBook(bookId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
