using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BookStoreModel
{
    public  class OrderModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string orderID { get; set; }

        [ForeignKey("BooksModel")]
        public string bookID { get; set; }
        public virtual BooksModel BooksModel { get; set; }

        [ForeignKey("RegisterModel")]
        public string userID { get; set; }
        public virtual RegisterModel RegisterModel { get; set; }

        [ForeignKey("AddressModel")]
        public string addressID { get; set; }
        public virtual AddressModel AddressModel { get; set; }

    }
}
