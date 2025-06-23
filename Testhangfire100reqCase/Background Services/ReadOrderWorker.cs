
using System;
using Testhangfire100reqCase.Models;
using Testhangfire100reqCase.orderQueue;

namespace Testhangfire100reqCase.Background_Services
{
    public class ReadOrderWorker : BackgroundService
    {


        private readonly ReadOrderQueue _queue;
        private readonly StoreDbcontext db; 

        public ReadOrderWorker(ReadOrderQueue queue , StoreDbcontext db)
        {
            _queue = queue;
        
            this.db = db;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                while (_queue.Queue.TryDequeue(out var request))
                {
                    

                    var order = await db.Orders.FindAsync(request.OrderId);
                    request.ResultSource.SetResult(order);  
                }

                await Task.Delay(100); 
            }
        }
    }
}
