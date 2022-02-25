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
    public class OrderController : ControllerBase
    {
        private readonly IOrderManager OM;
        public OrderController(IOrderManager manager)
        {
            this.OM = manager;
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody] OrderModel order)
        {
            try
            {
                var response = await this.OM.AddOrder(order);
                if (response != null)
                {
                    return this.Ok(new ResponseModel<OrderModel> { Status = true, Message = "Order Placed", Data = response });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Order not Placed" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }

        [HttpGet]
        public IActionResult GetOrder()
        {
            try
            {
                IEnumerable<OrderModel> response = this.OM.GetOrder();
                if (response != null)
                {
                    return this.Ok(new { Status = true, Message = "Order Retrived Successfully", Data = response });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Order is Empty" });
                }

            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }


        [HttpDelete]
        public async Task<IActionResult> CancleOrder([FromBody] OrderModel del)
        {
            try
            {
                var response = await this.OM.CancleOrder(del);
                if (response != false)
                {
                    return this.Ok(new ResponseModel<OrderModel> { Status = true, Message = "Order Canclled" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Order not Canclled" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }
    }
}
