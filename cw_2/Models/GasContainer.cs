using ContainerManagement.Interfaces;
using ContainerManagement.Exceptions;

namespace ContainerManagement.Models
{
    public class GasContainer : Container, IHazardNotifier
    {
         double pressure; // in atmospheres

        public GasContainer(double cargoWeight, double height, double depth, double capacity, double pressure)
            : base(Type.G, cargoWeight, height, depth, capacity)
        {
            this.pressure = pressure;
        }

        public void NotifyHazard(string message)
        {
            Console.WriteLine($"HAZARD NOTIFICATION for container {base.serialNumber}: {message}");
        }

        public new void EmptyingCargo()
        {
            // Leave 5% of cargo when emptying
            base.LoadCargo(base.cargoWeight * 0.05);
        }

        public new void LoadCargo(double weight)
        {
            if (weight > base.capacity)
            {
                NotifyHazard($"Attempted to load {weight}kg exceeding container capacity");
                throw new OverfillException("Cargo weight exceeds container capacity");
            }
            base.LoadCargo(weight);
        }
    }
} 