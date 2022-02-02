using Marketplace.Framework;

namespace Marketplace.Domain
{
    public class Money : Value<Money>
    {
        public decimal Amount { get; }

        public Money(decimal amount)
        {
            Amount = amount;
        }

        public Money Add(Money money)
        {
            return new Money(Amount + money.Amount);
        }

        public Money Subtract(Money money)
        {
            return new Money(Amount - money.Amount);
        }

        public static Money operator +(Money money1, Money money2)
        {
            return money1.Add(money2);
        }

        public static Money operator -(Money money1, Money money2)
        {
            return money1.Subtract(money2);
        }
    }
}
