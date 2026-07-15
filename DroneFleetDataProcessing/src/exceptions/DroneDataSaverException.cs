using System;
using System.Collections.Generic;
using System.Text;

namespace DroneFleetDataProcessing.src.exceptions
{
    class DroneDataSaverException: Exception
    {
        public DroneDataSaverException(string message)
            : base(message)
        {
        }
    }
}
