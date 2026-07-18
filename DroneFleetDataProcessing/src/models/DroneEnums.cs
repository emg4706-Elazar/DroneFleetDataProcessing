using System;
using System.Collections.Generic;
using System.Text;

namespace DroneFleetDataProcessing.src.models
{
    public enum DroneCategory { Recon, Patrol, Mapping, Delivery, Search };
    public enum DroneBaseLocation { North, South, Central, East, West };
    public enum DroneStatus { Operational, Maintenance, Grounded, Training };
}
