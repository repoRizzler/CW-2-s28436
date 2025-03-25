using ContainerManagement.Exceptions;

namespace ContainerManagement.Models
{
    public class CoolingContainer : Container
    {
         string productType;
         double temperature;
         Dictionary<string, double> productMinTemps = new Dictionary<string, double>
        {
            {"Bananas", 13.3},
            {"Chocolate", 18},
            {"Fish", 2},
            {"Meat", -15},
            {"Ice cream", -18},
            {"Frozen pizza", -30},
            // Add more products as needed
        };

        public CoolingContainer(double cargoWeight, double height, double depth, double capacity, 
            string productType, double temperature)
            : base(Type.C, cargoWeight, height, depth, capacity)
        {
            if (!productMinTemps.ContainsKey(productType))
            {
                throw new ArgumentException("Unknown product type");
            }

            if (temperature < productMinTemps[productType])
            {
                throw new ArgumentException($"Temperature too low for {productType}. Minimum required: {productMinTemps[productType]}°C");
            }

            this.productType = productType;
            this.temperature = temperature;
        }

        public new void LoadCargo(double weight)
        {
            // Only allow loading if container is empty or contains same product type
            if (base.cargoWeight > 0 && this.productType != productType)
            {
                throw new InvalidOperationException("Cannot mix different product types in cooling container");
            }
            base.LoadCargo(weight);
        }
    }
} 