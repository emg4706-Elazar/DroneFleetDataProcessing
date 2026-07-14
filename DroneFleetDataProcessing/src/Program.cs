using DroneFleetDataProcessing.src.services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DroneFleetDataProcessing.src.models;
using DroneFleetDataProcessing.src.exceptions;



namespace DroneFleetDataProcessing.src
{
    class Program
    {
        public static void Main()
        {
            string filename = "drones_raw.json";

            string filepath = Path.Combine(
                AppContext.BaseDirectory,
                "input",
                "raw",
                filename);


            DroneDataLoader loader = new DroneDataLoader();
            try
            {
                List<Drone> drones = loader.Load(filepath);
                Console.WriteLine("The Process was successfuly");
            }
            catch (DroneDataLoaderException e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }



        
    }
}
