using System;
using Testhangfire100reqCase.Models;
using Testhangfire100reqCase.orderQueue;

namespace Testhangfire100reqCase.Background_Services
{
    public class OrderProcessor :BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly OrderQueue _orderQueue;
        private readonly StoreDbcontext dbcontext; 
        public OrderProcessor(IServiceScopeFactory scopeFactory, OrderQueue queue)
        {
            _scopeFactory = scopeFactory;
            _orderQueue = queue;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                while (_orderQueue.Queue.TryDequeue(out var customerName))
                {
                   // using var scope = _scopeFactory.CreateScope();
                  //  var dbContext = scope.ServiceProvider.GetRequiredService<StoreDbcontext>();

                    var order = new Orders
                    {
                        CustomerName = customerName,
                        CreatedAt = DateTime.UtcNow
                    };


                    Console.WriteLine($" معالجة: {customerName} - {DateTime.Now}");

                    dbcontext.Orders.Add(order);
                    await dbcontext.SaveChangesAsync();
                }


                await Task.Delay(500); 
            }
        }
    }
}
