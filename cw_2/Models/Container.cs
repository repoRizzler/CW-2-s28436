using ContainerManagement.Exceptions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ContainerManagement.Models
{
    public class Container
    {
        public double cargoWeight; // kg 
        public double height; //  cm - centimeters
        public double depth; // cm - centimeters 
        public double capacity; // kg - kilograms
        public Type type;
        public string? serialNumber;

        public Container(Type type, double cargoWeight, double height, double depth, double capacity)
        {  
            this.cargoWeight = cargoWeight;
            this.height = height;
            this.depth = depth;
            this.capacity = capacity;
            this.type = type;
        }

        public void SetSerialNumber(int number)
        {
            this.serialNumber = "KON-" + type.ToString() + "-" + number;
        }

        public virtual void EmptyingCargo()
        {
            this.cargoWeight = 0;
        }

        public virtual void LoadCargo(double weight)
        {
            if (weight > capacity)
            {
                throw new OverfillException("Cargo weight exceeds container capacity");
            }
            this.cargoWeight = weight;
        }
    }
} 