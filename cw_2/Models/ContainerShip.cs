using ContainerManagement.Interfaces;
using ContainerManagement.Exceptions;

namespace ContainerManagement.Models
{
    public class ContainerShip
    {
        public List<Container> containers;
        public double maxSpeed; // in knots
        public int maxContainerCount;
        public double maxWeight; // in tons
        public string name;

        public ContainerShip(string name, double maxSpeed, int maxContainerCount, double maxWeight)
        {
            this.name = name;
            this.maxSpeed = maxSpeed;
            this.maxContainerCount = maxContainerCount;
            this.maxWeight = maxWeight;
            this.containers = new List<Container>();
        }

        public void LoadContainer(Container container)
        {
            if (containers.Count >= maxContainerCount)
            {
                throw new InvalidOperationException("Ship has reached maximum container capacity");
            }

            double currentTotalWeight = containers.Sum(c => c.cargoWeight) / 1000.0; // Convert kg to tons
            double newContainerWeight = container.cargoWeight / 1000.0; // Convert kg to tons

            if (currentTotalWeight + newContainerWeight > maxWeight)
            {
                throw new InvalidOperationException("Adding this container would exceed ship's maximum weight capacity");
            }

            containers.Add(container);
        }

        public void LoadContainers(List<Container> newContainers)
        {
            foreach (var container in newContainers)
            {
                LoadContainer(container);
            }
        }

        public void RemoveContainer(string serialNumber)
        {
            var container = containers.FirstOrDefault(c => c.serialNumber == serialNumber);
            if (container == null)
            {
                throw new InvalidOperationException($"Container with serial number {serialNumber} not found on ship");
            }
            containers.Remove(container);
        }

        public void ReplaceContainer(string serialNumber, Container newContainer)
        {
            var index = containers.FindIndex(c => c.serialNumber == serialNumber);
            if (index == -1)
            {
                throw new InvalidOperationException($"Container with serial number {serialNumber} not found on ship");
            }

            // Temporarily remove the old container to check weight limits
            var oldContainer = containers[index];
            containers.RemoveAt(index);

            try
            {
                LoadContainer(newContainer);
            }
            catch
            {
                // If loading fails, restore the old container
                containers.Insert(index, oldContainer);
                throw;
            }
        }

        public void TransferContainer(string serialNumber, ContainerShip targetShip)
        {
            var container = containers.FirstOrDefault(c => c.serialNumber == serialNumber);
            if (container == null)
            {
                throw new InvalidOperationException($"Container with serial number {serialNumber} not found on ship");
            }

            try
            {
                targetShip.LoadContainer(container);
                containers.Remove(container);
            }
            catch
            {
                throw new InvalidOperationException("Cannot transfer container to target ship due to capacity limits");
            }
        }

        public void PrintContainerInfo(string serialNumber)
        {
            var container = containers.FirstOrDefault(c => c.serialNumber == serialNumber);
            if (container == null)
            {
                throw new InvalidOperationException($"Container with serial number {serialNumber} not found on ship");
            }

            Console.WriteLine($"Container Information:");
            Console.WriteLine($"Serial Number: {container.serialNumber}");
            Console.WriteLine($"Type: {container.type}");
            Console.WriteLine($"Cargo Weight: {container.cargoWeight} kg");
            Console.WriteLine($"Height: {container.height} cm");
            Console.WriteLine($"Depth: {container.depth} cm");
            Console.WriteLine($"Capacity: {container.capacity} kg");
        }

        public void PrintShipInfo()
        {
            Console.WriteLine($"Ship Information:");
            Console.WriteLine($"Name: {name}");
            Console.WriteLine($"Max Speed: {maxSpeed} knots");
            Console.WriteLine($"Current Container Count: {containers.Count}/{maxContainerCount}");
            Console.WriteLine($"Current Total Weight: {containers.Sum(c => c.cargoWeight) / 1000.0:F2}/{maxWeight} tons");
            
            Console.WriteLine("\nContainers on board:");
            foreach (var container in containers)
            {
                Console.WriteLine($"- {container.serialNumber} ({container.type}) - {container.cargoWeight} kg");
            }
        }
    }
} 