using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookStoreModel
{
    public class RegisterModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string userID { get; set; }
        public string fullName { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9]+([.#_$+-][a-zA-Z0-9]+)*[@][a-zA-Z0-9]+[.][a-zA-Z]{2,3}([.][a-zA-Z]{2})?$", ErrorMessage = "Email is not valid. Please Enter valid email")]
        public string emailID { get; set; }

        [Required]
         public string password { get; set; }
        public string mobile { get; set; }
    }
}
