using System.Collections.Concurrent;
using Testhangfire100reqCase.Models;

namespace Testhangfire100reqCase.orderQueue
{
    public class ReadOrderQueue
    {

        public ConcurrentQueue<ReadOrderRequest> Queue { get; set; } = new();
    }
}
public class ReadOrderRequest
{
    public int OrderId { get; set; }
    public TaskCompletionSource<Orders?> ResultSource { get; set; } = new();
}
