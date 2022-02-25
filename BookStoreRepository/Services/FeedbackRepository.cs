using BookStoreModel;
using BookStoreRepository.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRepository.Services
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly IConfiguration _config;
        public readonly IMongoDatabase Db;
        public FeedbackRepository(IOptions<Setting> options, IConfiguration config)
        {
            _config = config;
            var client = new MongoClient(options.Value.Connectionstring);
            Db = client.GetDatabase(options.Value.DatabaseName);
        }
        public IMongoCollection<FeedbackModel> Feedback =>
            Db.GetCollection<FeedbackModel>("Feedback");

        public async Task<FeedbackModel> AddFeedback(FeedbackModel feed)
        {
            try
            {
                var check = await this.Feedback.Find(x => x.feedbackID == feed.feedbackID).SingleOrDefaultAsync();
                if (check == null)
                {
                    await this.Feedback.InsertOneAsync(feed);
                    return feed;

                }
                return null;

            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<FeedbackModel> GetFeedback()
        {
            return Feedback.Find(FilterDefinition<FeedbackModel>.Empty).ToList();
        }
    }
}
