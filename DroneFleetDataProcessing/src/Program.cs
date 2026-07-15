using DroneFleetDataProcessing.src.services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DroneFleetDataProcessing.src.models;
using DroneFleetDataProcessing.src.exceptions;
using DroneFleetDataProcessing.src.validators;



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
                //List<Drone> drones = loader.Load(filepath);
                List<Drone> allDrones = loader.Load(filepath);
                List<Drone> validDrones = new List<Drone>();
                List<Drone> rejectedDrones = new List<Drone>();

                var validator = new DroneValidator();
                var storer = new DronesDataValidator(validator);

                storer.ValidateFleet(allDrones, validDrones, rejectedDrones);

            }
            catch (DroneDataLoaderException e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }



        
    }
}
