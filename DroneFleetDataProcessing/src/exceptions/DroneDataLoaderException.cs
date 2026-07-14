using System;
using System.Collections.Generic;
using System.Text;

namespace DroneFleetDataProcessing.src.exceptions
{
    public class DroneDataLoaderException : Exception
    {
        public DroneDataLoaderException(string message)
            : base(message)
        {
        }

        public DroneDataLoaderException(
            string message,
            Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
