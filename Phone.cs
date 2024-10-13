using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace AssetTracking_Mini_Project_1
{
    class Phone : Asset // phone is child class to parent class asset
    {
        public Phone(string brand, string model, Country country, DateTime purchaseDate, Price purchasePrice)
            : base(brand, model, country, purchaseDate, purchasePrice) { } // method chaining parameters in constructor
    }
}