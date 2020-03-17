using System.Collections.Generic;

namespace ItsCoffee.Core.Entities
{
    public class Payments
    {
        public Dictionary<PaymentType, decimal> paymentAmounts;

        public Payments()
        {
            paymentAmounts = new Dictionary<PaymentType, decimal>();
        }

        public void AddPayment(PaymentType paymentType, decimal amount)
        {
            if (!paymentAmounts.ContainsKey(paymentType))
            {
                paymentAmounts.Add(paymentType, 0);
            }

            paymentAmounts[paymentType] += amount;
          
        }

    }

    public enum PaymentType
    { 
        Cash,
        Credit,
        LoyaltyPoints
    }
    
}