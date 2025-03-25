using ContainerManagement.Interfaces;
using ContainerManagement.Exceptions;

namespace ContainerManagement.Models
{
    public class LiquidContainer : Container, IHazardNotifier
    {
         bool isHazardous;

        public LiquidContainer(double cargoWeight, double height, double depth, double capacity, bool isHazardous) 
            : base(Type.L, cargoWeight, height, depth, capacity)
        {
            this.isHazardous = isHazardous;
        }

        public void NotifyHazard(string message)
        {
            Console.WriteLine($"HAZARD NOTIFICATION for container {base.serialNumber}: {message}");
        }

        public new void LoadCargo(double weight)
        {
            double maxFillLevel = isHazardous ? 0.5 : 0.9;
            double maxAllowedWeight = base.capacity * maxFillLevel;

            if (weight > maxAllowedWeight)
            {
                NotifyHazard($"Attempted to load {weight}kg exceeding {maxFillLevel * 100}% capacity limit");
                throw new OverfillException($"Cannot load more than {maxFillLevel * 100}% capacity for this container type");
            }
            base.LoadCargo(weight);
        }
    }
} 