using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using DroneFleetDataProcessing.src.models;
using DroneFleetDataProcessing.src.exceptions;
using DroneFleetDataProcessing.src.services;

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

                FileWriter writer = new FileWriter();
                writer.Write(filepathOutput, strToWrite);

            }
            catch(ArgumentNullException e)
            {
                throw new DroneDataSaverException(e.Message);
            }
            catch(InvalidPathException e)
            {
                throw new DroneDataSaverException(e.Message);
            }
            catch(UnauthorizedAccessException e)
            {
                throw new DroneDataSaverException(e.Message);
            }
            catch(IOException e)
            {
                throw new DroneDataSaverException(e.Message);
            }
        }

        
    }
}
