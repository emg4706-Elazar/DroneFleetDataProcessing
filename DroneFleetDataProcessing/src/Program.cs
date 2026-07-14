using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DroneFleetDataProcessing.src
{
    class Program
    {
        public static void Main()
        {
            string filepath = Path.Combine(
                AppContext.BaseDirectory,
                "raw",
                "drones_raw.json");

            string conFile = File.ReadAllText(filepath);
            Console.WriteLine(conFile);
        }
    }
}
