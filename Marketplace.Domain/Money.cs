using Marketplace.Framework;
using System;

namespace Marketplace.Domain
{
    public class Money : Value<Money>
    {
        private const string DefaultCurrency = "EUR";

        public static Money FromDecimal(decimal amount, string currency = DefaultCurrency)
        {
            return new Money(amount, currency);
        }

        public static Money FromString(string amount, string currency = DefaultCurrency)
        {
            return new Money(decimal.Parse(amount), currency);
        }

        public decimal Amount { get; }
        public string Currency { get; }

        protected Money(decimal amount, string currency = DefaultCurrency)
        {
            if (Math.Round(amount, 2) != amount)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount cannot have more than two decimals");
            }

            Amount = amount;
            Currency = currency;
        }

        public Money Add(Money money)
        {
            if (Currency != money.Currency)
            {
                throw new CurrencyMismatchException("Cannot sum amounts with different currencies");
            }

            return new Money(Amount + money.Amount);
        }

        public Money Subtract(Money money)
        {
            if (Currency != money.Currency)
            {
                throw new CurrencyMismatchException("Cannot subtract amounts with different currencies");
            }

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

    public class CurrencyMismatchException : Exception
    {
        public CurrencyMismatchException(string message)
            : base(message)
        {
        }
    }
}
