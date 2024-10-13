using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace AssetTracking_Mini_Project_1
{
    class AssetManager 
    {
        private AssetTracker tracker = new AssetTracker(); // encapsulate tracker 

        public void Run()
        {
            AddSampleAssets(); // for testing purposes

            while (true) // menu loop
            {
                ShowMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddNewAsset();
                        break;
                    case "2":
                        tracker.PrintAssets();
                        break;
                    case "3":
                        tracker.ClearList(); // for testing purposes
                        break;
                    case "4":
                        AddSampleAssets(); // for testing purposes
                        break;
                    case "5":
                        Console.WriteLine("Exiting program. Goodbye!");
                        Console.ReadKey();
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        private void ShowMenu()
        {
            Console.WriteLine("\nAsset Tracker Menu:");
            Console.WriteLine("1. Add a new asset");
            Console.WriteLine("2. View all assets");
            Console.WriteLine("3. Clear all assets");
            Console.WriteLine("4. Add sample assets");
            Console.WriteLine("5. Exit");
            Console.Write("Enter your choice (1-5): ");
        }

        private void AddNewAsset()
        {
            try // exception handling if problem adding new asset
            {
                string type = GetValidInput("Enter asset type (Computer/Phone): ",
                    input => input.Equals("computer", StringComparison.OrdinalIgnoreCase) || 
                    input.Equals("phone", StringComparison.OrdinalIgnoreCase));

                string brand = GetValidInput("Enter brand: ", input => !string.IsNullOrWhiteSpace(input));
                string model = GetValidInput("Enter model: ", input => !string.IsNullOrWhiteSpace(input));
                string countryName = GetValidInput("Enter country: ", input => !string.IsNullOrWhiteSpace(input));
               
                string countryCurrency = "";
                switch (countryName.ToUpper())
                {
                    case "USA":
                        countryCurrency = "USD";
                        break;
                    case "GERMANY":
                        countryCurrency = "EUR";
                        break;
                    case "SWEDEN":
                        countryCurrency = "SEK";
                        break;
                    default: break;
                }
                Country country = new Country(countryName, countryCurrency); 


                DateTime purchaseDate = GetValidDate("Enter purchase date (yyyy-MM-dd): ");
                decimal purchaseAmount = GetValidDecimal("Enter purchase amount: ");
                string currency = GetValidInput("Enter purchase currency (USD/EUR/SEK): ",
                    input => input.Equals("USD", StringComparison.OrdinalIgnoreCase) ||
                             input.Equals("EUR", StringComparison.OrdinalIgnoreCase) ||
                             input.Equals("SEK", StringComparison.OrdinalIgnoreCase));

                Price purchasePrice = new Price(purchaseAmount, currency);

                Asset newAsset;
                if (type.Equals("computer", StringComparison.OrdinalIgnoreCase))
                    newAsset = new Computer(brand, model, country, purchaseDate, purchasePrice);
                else
                    newAsset = new Phone(brand, model, country, purchaseDate, purchasePrice);

                tracker.AddAsset(newAsset);
                Console.WriteLine("Asset added successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding asset: {ex.Message}");
            }
        }

        // some methods for validation of input
        private string GetValidInput(string prompt, Func<string, bool> isValid)
        {
            string input;
            do
            {
                Console.Write(prompt);
                input = Console.ReadLine();
                if (!isValid(input))
                {
                    Console.WriteLine("Invalid input. Please try again.");
                }
            } while (!isValid(input));
            return input.Trim();
        }

        private DateTime GetValidDate(string prompt)
        {
            while (true)
            {
                string input = GetValidInput(prompt, s => DateTime.TryParseExact(s, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _));
                if (DateTime.TryParseExact(input, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
                {
                    return result;
                }
                Console.WriteLine("Invalid date format. Please enter the date in yyyy-MM-dd format.");
            }
        }

        private decimal GetValidDecimal(string prompt)
        {
            while (true)
            {
                string input = GetValidInput(prompt, s => decimal.TryParse(s, out _));
                if (decimal.TryParse(input, out decimal result))
                {
                    return result;
                }
                Console.WriteLine("Invalid decimal number. Please try again.");
            }
        }

        // sample data for testing purposes
        private void AddSampleAssets()
        {

            tracker.AddAsset(new Phone("Apple", "iPhone", new Country("USA", "USD"), DateTime.Now.AddMonths(-33), new Price(800, "USD")));
            tracker.AddAsset(new Phone("Apple", "iPhone", new Country("USA", "USD"), DateTime.Now.AddMonths(-30), new Price(800, "USD")));
            tracker.AddAsset(new Phone("Apple", "iPhone", new Country("USA", "USD"), DateTime.Now.AddMonths(-59), new Price(800, "USD")));
            tracker.AddAsset(new Phone("Apple", "iPhone", new Country("USA", "USD"), DateTime.Now.AddMonths(-100), new Price(800, "USD")));

            tracker.AddAsset(new Phone("apple", "iPhone", new Country("Germany", "usd"), DateTime.Now.AddMonths(-32), new Price(830, "eur")));
            tracker.AddAsset(new Phone("apple", "iPhone", new Country("Germany", "usd"), DateTime.Now.AddMonths(-25), new Price(830, "eur")));
            tracker.AddAsset(new Phone("apple", "iPhone", new Country("Germany", "usd"), DateTime.Now.AddMonths(-70), new Price(830, "eur")));
            
            tracker.AddAsset(new Phone("apple", "iPhone", new Country("Sweden", "usd"), DateTime.Now.AddMonths(-95), new Price(8500, "sek")));
            tracker.AddAsset(new Phone("apple", "iPhone", new Country("Sweden", "usd"), DateTime.Now.AddMonths(-36), new Price(8500, "sek")));
            tracker.AddAsset(new Phone("apple", "iPhone", new Country("Sweden", "usd"), DateTime.Now.AddMonths(-15), new Price(8500, "sek")));

        }
    }
}