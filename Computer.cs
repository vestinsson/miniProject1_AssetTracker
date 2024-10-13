using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace AssetTracking_Mini_Project_1
{
    class Computer : Asset // computer is subclass to asset
    {
        public Computer(string brand, string model, Country country, DateTime purchaseDate, Price purchasePrice)
            : base(brand, model, country, purchaseDate, purchasePrice) { } // constructor chaining the superclass
    }
}
