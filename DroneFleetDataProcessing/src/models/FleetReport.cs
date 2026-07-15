using System;
using System.Collections.Generic;
using System.Text;

namespace DroneFleetDataProcessing.src.models
{
    class FleetReport
    {
        public int TotalRecords { get; set; }
        public int ValidRecords { get; set; }
        public int RejectedRecords { get; set; }
        public List<string> NonOperationalDrones { get; set; }
        public List<string> TopFiveDroneByFlightHours { get; set; }
        public List<string> AvailableModels { get; set; }
        public Dictionary<string, int> DroneByBase { get; set; }
        public Dictionary<string, double> AverageBatteryByModel { get; set; }
        public string BestModelByMissions { get; set; }
        public int BestModelTotalMissions { get; set; }
        public List<string> AdditionalAnalysisLines { get; set; }


    }

}
