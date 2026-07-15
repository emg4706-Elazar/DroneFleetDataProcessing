using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Text.Json;
using DroneFleetDataProcessing.src.exceptions;
using DroneFleetDataProcessing.src.models;



namespace DroneFleetDataProcessing.src.services
{

    // manage the process of read and convert the file
    class DroneDataLoader
    {

        // Recieve filepath and return all file's context or match exception.
        public List<Drone> Load(string filename)
        {
            string filepathInput = GetInputPath(filename);
            try
            {
                ValidatePath(filepathInput);
                string contextFile = File.ReadAllText(filepathInput);
                ValidateJsonContent(contextFile);

                List<Drone>? drones = JsonSerializer.Deserialize<List<Drone>>(contextFile);
                ValidateDecerialized(drones);

                return drones;
            }



            catch(InvalidPathException e)
            {
                throw new DroneDataLoaderException(
                    "The path coudn't be empty",
                    e);
            }

            catch (FileNotFoundException e)
            {
                throw new DroneDataLoaderException(
                    "The input file was not found",
                    e);
            }
            catch (EmptyFileException e)
            {
                throw new DroneDataLoaderException(
                    "file Coudn't be empty.",
                    e);
            }
            catch(InvalidJsonException e)
            {
                throw new DroneDataLoaderException(
                    "The input file contains invalid JSON.",
                    e);
            }
            catch (UnauthorizedAccessException e)
            {
                throw new DroneDataLoaderException(
                    "The program does not have permission to read the input file",
                    e);
            }
            catch(JsonException e)
            {
                throw new DroneDataLoaderException(
                    "Syntax error in this file.",
                    e);
            }
            catch (EmptyListException e)
            {
                throw new DroneDataLoaderException(
                    "Empty List found.",
                    e);
            }
    
        }



        // return exception if the file not exist
        private void ValidatePath(string filepath)
        {
            if (String.IsNullOrWhiteSpace(filepath))
            {
                throw new InvalidPathException();
            }

            if (!File.Exists(filepath))
            {
                throw new FileNotFoundException();
            }
        
        }

        // return exception if the file is empty
        private void ValidateJsonContent(string contextFile)
        {
            if (String.IsNullOrWhiteSpace(contextFile))
            {
                throw new EmptyFileException();
            }

        }

     
        // return exception if the list is null or empty
        private void ValidateDecerialized(List<Drone>? drones)
        {

            if (drones is null)
            {
                throw new InvalidJsonException();
            }
            if (drones.Count == 0)
            {
                throw new EmptyListException();
            }

        }
        private static string GetInputPath(string filename)
        {
            string filepath = Path.Combine(
                AppContext.BaseDirectory,
                "input",
                "raw",
                filename);

            return filepath;
        }
    }
}
