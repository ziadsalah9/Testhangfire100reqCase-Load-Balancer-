
using System;
using Testhangfire100reqCase.Models;
using Testhangfire100reqCase.orderQueue;

namespace Testhangfire100reqCase.Background_Services
{
    public class ReadOrderWorker : BackgroundService
    {

        private readonly IServiceScopeFactory _scopeFactory;

        private readonly ReadOrderQueue _queue;

        public ReadOrderWorker(ReadOrderQueue queue , IServiceScopeFactory scopeFactory)
        {
            _queue = queue;
        

            _scopeFactory = scopeFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                while (_queue.Queue.TryDequeue(out var request))
                {

                    using var scope = _scopeFactory.CreateScope();
                    var db = scope.ServiceProvider.GetRequiredService<StoreDbcontext>();

                    var order = await db.Orders.FindAsync(request.OrderId);
                    request.ResultSource.SetResult(order);  
                }

                await Task.Delay(100); 
            }
        }
    }
}
