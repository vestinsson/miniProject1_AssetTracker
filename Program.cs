using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace AssetTracking_Mini_Project_1
{
    class Program
    {
        static void Main(string[] args)
        {
            // creates and start an assset manager app
            AssetManager manager = new AssetManager();
            manager.Run();
        }
    }
}
