using System;
using System.Collections.Generic;
using System.Text;
using DroneFleetDataProcessing.src.models;

namespace DroneFleetDataProcessing.src.validators
{
    interface IDroneValidator
    {
       public bool IsValid(Drone drone);
    }
}
