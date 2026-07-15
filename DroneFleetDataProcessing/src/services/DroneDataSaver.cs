using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using DroneFleetDataProcessing.src.models;
using DroneFleetDataProcessing.src.exceptions;

namespace DroneFleetDataProcessing.src.services
{
    class DroneDataSaver
    {
        public void Save(List<Drone> drones, string filepathOutput)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };

                string strToWrite = JsonSerializer.Serialize(drones, options);
                
                File.WriteAllText(filepathOutput, strToWrite);

            }
            catch(Exception e)
            {
                throw new DroneDataSaverException(e.Message);
            }
        }

        
    }
}
