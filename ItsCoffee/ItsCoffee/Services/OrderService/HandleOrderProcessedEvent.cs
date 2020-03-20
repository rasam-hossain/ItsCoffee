using ItsCoffee.Core.Entities;

namespace ItsCoffee.Core.Services
{
    public delegate void OrderProcessedHandler(Order order);

    public class HandleOrderProcessedEvent
    {
        public event OrderProcessedHandler OrderProcessed;

        public void OrderCompleted(Order order)
        {
            if (OrderProcessed != null)
            {
                OrderProcessed(order);
            }
        }
    }
}