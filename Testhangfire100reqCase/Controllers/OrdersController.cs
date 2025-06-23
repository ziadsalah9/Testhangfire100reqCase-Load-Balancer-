using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Testhangfire100reqCase.Models;
using Testhangfire100reqCase.orderQueue;

namespace Testhangfire100reqCase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderQueue _orderQueue;
        private readonly ReadOrderQueue _readQueue;

        public OrdersController(OrderQueue orderQueue  , ReadOrderQueue readQueue)
        {
            _orderQueue = orderQueue;
            _readQueue = readQueue;
        }

        [HttpPost]
        public IActionResult Post([FromBody] OrderDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.CustomerName))
                return BadRequest("CustomerName is required");

            _orderQueue.Queue.Enqueue(dto.CustomerName);
            Console.WriteLine($"طلب جديد: {dto.CustomerName}");


            return Ok("تم الاستلام");
        }

        [HttpGet("stats")]
        public IActionResult GetStats()
        {
            int count = _orderQueue.Queue.Count;
            return Ok(new { QueueLength = count });
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var request = new ReadOrderRequest
            {
                OrderId = id
            };

            _readQueue.Queue.Enqueue(request);

            var order = await request.ResultSource.Task;

            if (order == null)
                return NotFound();

            return Ok(order);
        }

    }
}

public class OrderDto
{
    public string CustomerName { get; set; }
}

//public class ReadOrderRequest
//{
//    public int OrderId { get; set; }
//    public TaskCompletionSource<Orders?> ResultSource { get; set; } = new();
//}