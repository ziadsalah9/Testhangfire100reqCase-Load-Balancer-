using System.Collections.Concurrent;
using Testhangfire100reqCase.Models;

namespace Testhangfire100reqCase.orderQueue
{
    public class OrderQueue
    {
        public ConcurrentQueue<string> Queue  { get; set; } = new();


    }
}
