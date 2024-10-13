using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace AssetTracking_Mini_Project_1
{
    class AssetTracker
    {
        private List<Asset> assets = new List<Asset>(); // encapsulate the assets list

        public void AddAsset(Asset asset)
        {
            if (asset == null)
                throw new ArgumentNullException(nameof(asset));
            assets.Add(asset);
        }

        public void PrintAssets()
        {
            if (assets.Count == 0)
            {
                Console.WriteLine("No assets to display.");
                return;
            }
            // sorts the assets tracker list primary by country( a.k.a office) secondary by purchase date
            var sortedAssets = assets.OrderBy(a => a.Country).ThenBy(a => a.PurchaseDate).ToList();

            // prints the tabell header 
            Console.WriteLine("Type".PadRight(10) + 
                              "Brand".PadRight(10) + 
                              "Model".PadRight(10) +
                              "Office".PadRight(10) + // Country
                              "Purchase Date".PadRight(20) +
                              "Price (USD)".PadRight(15) +
                              "Currency".PadRight(10) +
                              //"Purchase Price".PadRight(15) + 
                              //"Current Value".PadRight(15) + 
                              "Current Value (Local)".PadRight(20) 
                              );

            foreach (var asset in sortedAssets)
            {   // calculates the numbers of months with the factor of 30.44 
                int monthsSincePurchase = (int)((DateTime.Now - asset.PurchaseDate).TotalDays / 30.44);


                if (monthsSincePurchase >= 33) // less than three months left for the asset
                    Console.ForegroundColor = ConsoleColor.Red;
                else if (monthsSincePurchase >= 30) // less than six months left for the asset
                    Console.ForegroundColor = ConsoleColor.Yellow;

                // prints the data tabell
                Console.WriteLine($"{asset.GetType().Name.PadRight(10)}" + // type
                                  $"{asset.Brand.PadRight(10)}" +
                                  $"{asset.Model.PadRight(10)}" +
                                  $"{asset.Country.Name.PadRight(10)}" + // office
                                  $"{asset.PurchaseDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture).PadRight(20)}" +
                                  $"{asset.PurchasePrice.GetAmountInUSD().ToString("F2", CultureInfo.InvariantCulture)} USD".PadRight(15) +
                                  $"{asset.Country.Currency.PadRight(10)}" +
                                  //$"{asset.PurchasePrice.ToString().PadRight(15)}" +
                                  $"{asset.GetCurrentValueInUSD().ToString().PadRight(20)}" 
                                 // $"{asset.GetCurrentValueInLocalCurrency().ToString().PadRight(20)}"
                                  );

                Console.ResetColor();
            }
        }

        public void ClearList() // zap the list for testing purposes
        {
            assets.Clear();
        }
    }
}