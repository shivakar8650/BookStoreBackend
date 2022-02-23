using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreModel
{
    public  class LoginResponse
    {
        
        public string userID { get; set; }
        public string fullName { get; set; }
        public string emailID { get; set; }
        public string mobile { get; set; }
        public string token { get; set; }

    }
}
