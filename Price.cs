using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace AssetTracking_Mini_Project_1
{
    class Price
    {
        public decimal Amount { get; }
        public string Currency { get; }

        // testing using generic class dictionary in c#, key is currency value is exchange rate
        private static readonly Dictionary<string, decimal> ExchangeRates = new Dictionary<string, decimal>
    {
        { "USD", 1m },
        { "EUR", 0.91m },
        { "SEK", 10.37m }
    };

        public Price(decimal amount, string currency)
        {
            Amount = amount;
            Currency = currency.ToUpperInvariant();
        }

        public Price ConvertTo(string targetCurrency) // constructor converts currency based on the exchange rates
        {
            targetCurrency = targetCurrency.ToUpperInvariant();
            if (Currency == targetCurrency)
                return this;

            if (!ExchangeRates.TryGetValue(Currency, out decimal sourceRate) ||
                !ExchangeRates.TryGetValue(targetCurrency, out decimal targetRate))
            {
                throw new ArgumentException("Invalid currency for conversion.");
            }

            decimal amountInUSD = Amount / sourceRate;
            decimal convertedAmount = amountInUSD * targetRate;

            return new Price(Math.Round(convertedAmount, 2), targetCurrency);
        }

        public decimal GetAmountInUSD() // method return $$$
        {
            return ConvertTo("USD").Amount;
        }

        // lambda function override tostring() return country currency
        public override string ToString() => $"{Amount.ToString("F2", CultureInfo.InvariantCulture)} {Currency}";
    }
}