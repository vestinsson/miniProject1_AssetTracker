using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace AssetTracking_Mini_Project_1
{
    class Country : IComparable<Country> // derives  interface for implentation of compareto
    {
        public string Name { get; set; }
        public string Currency { get; set; }

        public Country(string name, string currency) // constructor link country to its currency
        {
            Name = name;
            Currency = currency.ToUpperInvariant();
        }

        public int CompareTo(Country other) // implentation for comparing two countries name
        {
            return other == null ? 1 : this.Name.CompareTo(other.Name); // using condional operator ? :
        }
    }
}
