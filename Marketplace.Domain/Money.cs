using Marketplace.Framework;
using System;

namespace Marketplace.Domain
{
    public class Money : Value<Money>
    {
        private const string DefaultCurrency = "EUR";

        public static Money FromDecimal(decimal amount, string currency, ICurrencyLookup currencyLookup)
        {
            return new Money(amount, currency, currencyLookup);
        }

        public static Money FromString(string amount, string currency, ICurrencyLookup currencyLookup)
        {
            return new Money(decimal.Parse(amount), currency, currencyLookup);
        }

        private Money(decimal amount, CurrencyDetails currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public decimal Amount { get; }
        public CurrencyDetails Currency { get; }

        protected Money(decimal amount, string currencyCode, ICurrencyLookup currencyLookup)
        {
            if (string.IsNullOrEmpty(currencyCode))
            {
                throw new ArgumentNullException(nameof(currencyCode), "Currency code must be specified");
            }

            var currency = currencyLookup.FindCurrency(currencyCode);
            if (!currency.InUse)
            {
                throw new ArgumentException($"Currency {currencyCode} is not valid.");
            }

            if (Math.Round(amount, currency.DecimalPlaces) != amount)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), $"Amount in {currencyCode} cannot have more than {currency.DecimalPlaces} decimals");
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

            return new Money(Amount + money.Amount, Currency);
        }

        public Money Subtract(Money money)
        {
            if (Currency != money.Currency)
            {
                throw new CurrencyMismatchException("Cannot subtract amounts with different currencies");
            }

            return new Money(Amount - money.Amount, Currency);
        }

        public static Money operator +(Money money1, Money money2)
        {
            return money1.Add(money2);
        }

        public static Money operator -(Money money1, Money money2)
        {
            return money1.Subtract(money2);
        }

        public override string ToString()
        {
            return $"{Currency.CurrencyCode} {Amount}";
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
