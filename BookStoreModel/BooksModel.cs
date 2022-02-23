using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreModel
{
    public class BooksModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string bookID { get; set; }
        public string bookName { get; set; }
        public string authorName { get; set; }
        public decimal rating { get; set; }
        public int totalRating { get; set; }
        public int discountPrice { get; set; }
        public int originalPrice { get; set; }
        public string description { get; set; }
        public string bookImage { get; set; }
    }
}
