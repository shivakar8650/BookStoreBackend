using BookStoreManager.Interface;
using BookStoreModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackManager FM;
        public FeedbackController(IFeedbackManager manager)
        {
            this.FM = manager;
        }

        [HttpPost]
        public async Task<IActionResult> AddFeedback([FromBody] FeedbackModel feed)
        {
            try
            {
                var response = await this.FM.AddFeedback(feed);
                if (response != null)
                {
                    return this.Ok(new ResponseModel<FeedbackModel> { Status = true, Message = "Feedback Added succesfully", Data = response });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Feedback not Added" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }

        [HttpGet]
        public IActionResult GetFeedback()
        {
            try
            {
                IEnumerable<FeedbackModel> response = this.FM.GetFeedback();
                if (response != null)
                {
                    return this.Ok(new { Status = true, Message = "Feedback Retrived Successfully", Data = response });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Feedback is Empty" });
                }

            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }

    }
}
