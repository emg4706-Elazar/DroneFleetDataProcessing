using System;
using System.Collections.Generic;
using System.Text;

namespace DroneFleetDataProcessing.src.exceptions
{
    class DroneDataReporterException: Exception
    {
        public DroneDataReporterException(string message)
            : base(message)
        {
        }

        public DroneDataReporterException(
            string message,
            Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
