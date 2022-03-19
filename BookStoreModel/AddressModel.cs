using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BookStoreModel
{
    public class AddressModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string addressID { get; set; }

        [ForeignKey("RegisterModel")]
        public string userID { get; set; }
        public virtual RegisterModel RegisterModel { get; set; }

        [ForeignKey("AddressType")]
        public string fullname { get; set; }
        public string mobile { get; set; }
        public string pinCode { get; set; }
        public string locality { get; set; }
     
        public string address { get; set; }
        public string city { get; set; }
        public string landmark { get; set; }

        public string type { get; set; }


    }
}
