using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BookStoreModel
{
    public class FeedbackModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string feedbackID { get; set; }

        [ForeignKey("BooksModel")]
        public string bookID { get; set; }
        public virtual BooksModel BooksModel { get; set; }

        [ForeignKey("RegisterModel")]
        public string userID { get; set; }
        public virtual RegisterModel RegisterModel { get; set; }

        public int rating { get; set; }
        public string review { get; set; }
    }
}
