using BookStoreModel;
using BookStoreRepository.Interface;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRepository.Services
{
    public class BookRepository : IBookRepository
    {
        private readonly IConfiguration _config;
        public readonly IMongoDatabase Db;
        public BookRepository(IOptions<Setting> options, IConfiguration config)
        {
            _config = config;
            var client = new MongoClient(options.Value.Connectionstring);
            Db = client.GetDatabase(options.Value.DatabaseName);
        }
        public IMongoCollection<BooksModel> Books =>
            Db.GetCollection<BooksModel>("Books");

        public async Task<BooksModel> AddBook(BooksModel book)
        {
            try
            {
                var check = await this.Books.Find(x => x.bookID == book.bookID).SingleOrDefaultAsync();
                if (check == null)
                {
                    await this.Books.InsertOneAsync(book);
                    return book;
                }
                return null;

            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<BooksModel> Update(BooksModel book)
        {
            try
            {

                var getbook = await this.Books.Find(x => x.bookID == book.bookID).FirstOrDefaultAsync();
                if ( getbook!= null)
                {
                    await this.Books.UpdateOneAsync(x => x.bookID == book.bookID,
                        Builders<BooksModel>.Update.Set(x => x.bookName, book.bookName)
                        .Set(x => x.description, book.description)
                        .Set(x => x.authorName, book.authorName)
                        .Set(x => x.rating, book.rating)
                        .Set(x => x.totalRating, book.totalRating)
                        .Set(x => x.discountPrice, book.discountPrice));
                    var response =  await this.Books.Find(x => x.bookID == book.bookID).FirstOrDefaultAsync();
                    return response;

                   /* Books.ReplaceOne(filter: x => x.bookID == book.bookID, replacement: book);
                    return book;*/
                }
                else
                {
                    await this.Books.InsertOneAsync(book);
                    return book;
                }
            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }

        public BooksModel FindBook(string id)
        {
            return Books.Find(x => x.bookID == id).FirstOrDefault();
        }

        public IEnumerable<BooksModel> GetAllBooks()
        {
            return Books.Find(FilterDefinition<BooksModel>.Empty).ToList();
        }

   

        public async Task<BooksModel> Addimage(string bookId, IFormFile image)
        {
            try
            {
                var ifExists = await this.Books.AsQueryable().Where(x => x.bookID == bookId).SingleOrDefaultAsync();
                if (ifExists != null)
                {
                    Account account = new Account
                    (
                    _config["CloudinaryAccount:CloudName"],
                    _config["CloudinaryAccount:ApiKey"],
                    _config["CloudinaryAccount:ApiSecret"]
                    );
                    var path = image.OpenReadStream();
                    Cloudinary cloudinary = new Cloudinary(account);
                    ImageUploadParams uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(image.FileName, path)
                    };
                    var uploadResult = cloudinary.Upload(uploadParams);

                    ifExists.bookImage = uploadResult.Url.ToString();
                    await this.Books.UpdateOneAsync(x => x.bookID == bookId,
                       Builders<BooksModel>.Update.Set(x => x.bookImage, ifExists.bookImage));
                    return ifExists;
                }
                else
                    return null;
            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> DeleteBook(string bookId)
        {
            try
            {
                var ifExists = await this.Books.FindOneAndDeleteAsync(x => x.bookID == bookId);
                return true;

            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
