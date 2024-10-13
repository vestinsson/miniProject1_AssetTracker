using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace AssetTracking_Mini_Project_1
{
    abstract class Asset
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public Country Country { get; set; }
        public DateTime PurchaseDate { get; set; }
        public Price PurchasePrice { get; set; }
        public decimal CurrentValue { get; set; }

        protected Asset(string brand, string model, Country country, DateTime purchaseDate, Price purchasePrice)
        {
            Brand = brand;
            Model = model;
            Country = country ?? throw new ArgumentNullException(nameof(country));
            PurchaseDate = purchaseDate;
            PurchasePrice = purchasePrice?.ConvertTo(country.Currency) ?? throw new ArgumentNullException(nameof(purchasePrice));
            CurrentValue = CalculateCurrentValue();
        }

        private decimal CalculateCurrentValue()
        {
            double monthsSincePurchase = (DateTime.Now - PurchaseDate).TotalDays / 30.44; // Average days in a month
            decimal depreciation = 0.01m * (decimal)monthsSincePurchase;
            return Math.Max(0, PurchasePrice.Amount * (1 - depreciation));
        }

        public Price GetCurrentValueInUSD()
        {
            return new Price(CurrentValue, Country.Currency).ConvertTo("USD");
        }

        public Price GetCurrentValueInLocalCurrency()
        {
            return new Price(CurrentValue, Country.Currency);
        }
    }
}