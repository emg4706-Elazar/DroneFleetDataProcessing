using DroneFleetDataProcessing.src.models;
using DroneFleetDataProcessing.src.validators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DroneFleetDataProcessing.src.services
{
    class DronesDataValidator
    {
        private readonly IDroneValidator _validator;

        public DronesDataValidator(IDroneValidator validator)
            {
            _validator = validator;
            }

        public void ValidateFleet(List<Drone> allDrones, List<Drone> validDrones, List<Drone> rejectedDrones)
        {
            var duplicateIds = allDrones.GroupBy(d => d.id).Where(d => d.Count() > 1).Select(d => d.Key).ToHashSet();
            var duplicateSerialNumbers = allDrones.GroupBy(d => d.serialNumber).Where(d => d.Count() > 1).Select(d => d.Key).ToHashSet();


            foreach (Drone drone in allDrones)
            {

                if (duplicateIds.Contains(drone.id) || duplicateSerialNumbers.Contains(drone.serialNumber))
                {
                    rejectedDrones.Add(drone);
                }

                else if (_validator.IsValid(drone))
                {
                    validDrones.Add(drone);
                }

                else { rejectedDrones.Add(drone); } 

            }
        }
    }
}
