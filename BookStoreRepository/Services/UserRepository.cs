using BookStoreModel;
using BookStoreRepository.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRepository.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _config;
        public readonly IMongoDatabase Db;
        public UserRepository(IOptions<Setting> options, IConfiguration config)
        {
            _config = config;
            var client = new MongoClient(options.Value.Connectionstring);
            Db = client.GetDatabase(options.Value.DatabaseName);
        }
        public IMongoCollection<RegisterModel> User =>
            Db.GetCollection<RegisterModel>("User");

        public async Task<RegisterModel> Register(RegisterModel register)
        {
            register.password = Encrypt(register.password);
            try
            {
                var check = await this.User.AsQueryable().Where(x => x.emailID == register.emailID).SingleOrDefaultAsync();
                if (check == null)
                {
                    await this.User.InsertOneAsync(register);
                    return register;

                }
                return null;

            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }


        public async Task<LoginResponse> Login(LoginModel login)
        {
            try
            {
                var loginUser = await this.User.AsQueryable().Where(x => x.emailID == login.emailID).FirstOrDefaultAsync();
                if (Decryptpass(loginUser.password) == login.password)
                {
                    string token = "";
                    LoginResponse Response = new LoginResponse();
                    token = GenerateJWTToken(loginUser.emailID, loginUser.userID);
                    Response.userID = loginUser.userID;
                    Response.fullName= loginUser.fullName;
                    Response.emailID = loginUser.emailID;
                    Response.token = token;
                    Response.mobile = loginUser.mobile;
                        
                    return Response;
                }
                return null;

            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }


        public async Task<bool> Forget(ForgetModel fmodel)
        {
            try
            {
                var ForgetUser = await this.User.AsQueryable().Where(x => x.emailID == fmodel.emailID).FirstOrDefaultAsync();
                if (ForgetUser.emailID != null)
                {
                    var token = GenerateJWTToken(ForgetUser.emailID, ForgetUser.userID);
                    new MsmqOperation().Sender(token);
                    return true;

                }
                return false;

            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }


        public async Task<RegisterModel> Reset(ResetModel reset)
        {
            try
            {
                var resetuser = await this.User.AsQueryable().Where(x => x.emailID == reset.emailID).FirstOrDefaultAsync();
                if (resetuser.emailID != null)
                {
                    await this.User.UpdateOneAsync(x => x.emailID == reset.emailID,
                        Builders<RegisterModel>.Update.Set(x => x.password, Encrypt(reset.newpassword)));
             
                    var response = await this.User.AsQueryable().Where(x => x.emailID == reset.emailID).FirstOrDefaultAsync();
                    return response;

                }
                return null;

            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }

       

        public static string Encrypt(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }

        private string GenerateJWTToken(string EmailId, string UserId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
           new Claim(ClaimTypes.Email,EmailId),
           new Claim("UserId",UserId
           )
           };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"], EmailId,
              claims,
              expires: DateTime.Now.AddMinutes(50),
              signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string Decryptpass(string encryptpwd)
        {
            string decryptpwd = string.Empty;
            UTF8Encoding encodepwd = new UTF8Encoding();
            Decoder Decode = encodepwd.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encryptpwd);
            int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            decryptpwd = new String(decoded_char);
            return decryptpwd;
        }
    }
}
