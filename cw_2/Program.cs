using System;
using System.Collections.Generic;
using ContainerManagement.Models;
using ContainerManagement.Interfaces;
using ContainerManagement.Exceptions;

namespace ContainerShipManagementSystem
{
    class Program
    {
        static List<ContainerShip> ships = new List<ContainerShip>();
        static List<Container> containers = new List<Container>();
        private static int containerCounter = 1;
        static void LoadContainerToShip()
        {
            if (ships.Count == 0 || containers.Count == 0)
            {
                Console.WriteLine("Brak dostępnych statków lub kontenerów.");
                return;
            }

            Console.Clear();
            Console.WriteLine("=== Ładowanie kontenera na statek ===\n");

            Console.WriteLine("Wybierz statek:");
            for (int i = 0; i < ships.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {ships[i].name}");
            }
            int shipIndex = int.Parse(Console.ReadLine()) - 1;

            Console.WriteLine("\nWybierz kontener:");
            for (int i = 0; i < containers.Count; i++)
            {
                // Check if container is already on any ship
                bool isOnShip = false;
                foreach (var ship in ships)
                {
                    if (ship.containers.Any(c => c.serialNumber == containers[i].serialNumber))
                    {
                        isOnShip = true;
                        break;
                    }
                }
                if (!isOnShip)
                {
                    Console.WriteLine($"{i + 1}. {containers[i].serialNumber} ({containers[i].type})");
                }            
            }
            int containerIndex = int.Parse(Console.ReadLine()) - 1;

            if (shipIndex >= 0 && shipIndex < ships.Count && containerIndex >= 0 && containerIndex < containers.Count)
            {
                var container = containers[containerIndex];

                // Check if container is already on any ship
                foreach (var ship in ships)
                {
                    if (ship.containers.Any(c => c.serialNumber == container.serialNumber))
                    {
                        Console.WriteLine($"\nBłąd: Kontener {container.serialNumber} jest już załadowany na statek {ship.name}");
                        return;
                    }
                }

                Console.WriteLine($"\nPojemność kontenera: {container.capacity} kg");
                Console.Write("Podaj wagę ładunku (w kg): ");
                double cargoWeight = double.Parse(Console.ReadLine());

                try
                {
                    container.LoadCargo(cargoWeight);
                    ships[shipIndex].LoadContainer(container);
                    Console.WriteLine("Kontener został załadowany pomyślnie!");
                }
                catch (OverfillException ex)
                {
                    Console.WriteLine($"Błąd: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Błąd: {ex.Message}");
                }
            }
        }

        static void LoadCargoToContainer()
        {
            if (containers.Count == 0)
            {
                Console.WriteLine("Brak dostępnych kontenerów.");
                return;
            }

            Console.Clear();
            Console.WriteLine("=== Załadunek ładunku do kontenera ===\n");

            Console.WriteLine("Wybierz kontener:");
            for (int i = 0; i < containers.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {containers[i].serialNumber} ({containers[i].type}) - Pojemność: {containers[i].capacity} kg");
            }
            int containerIndex = int.Parse(Console.ReadLine()) - 1;

            if (containerIndex >= 0 && containerIndex < containers.Count)
            {
                var container = containers[containerIndex];
                Console.Write("\nPodaj wagę ładunku (w kg): ");
                double cargoWeight = double.Parse(Console.ReadLine());

                try
                {
                    container.LoadCargo(cargoWeight);
                    Console.WriteLine("Ładunek został załadowany pomyślnie!");
                    Console.WriteLine($"Aktualna waga ładunku: {container.cargoWeight} kg");
                }
                catch (OverfillException ex)
                {
                    Console.WriteLine($"Błąd: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Błąd: {ex.Message}");
                }
            }
        }

        static void DisplayMainMenu()
        {
            Console.Clear();
            Console.WriteLine("=== Container Ship Management System ===\n");

            Console.WriteLine("Lista kontenerowców:");
            if (ships.Count == 0)
                Console.WriteLine("Brak");
            else
            {
                for (int i = 0; i < ships.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {ships[i].name} (speed={ships[i].maxSpeed}, maxContainerNum={ships[i].maxContainerCount}, maxWeight={ships[i].maxWeight})");
                }
            }

            Console.WriteLine("\nLista kontenerów:");
            if (containers.Count == 0)
                Console.WriteLine("Brak");
            else
            {
                for (int i = 0; i < containers.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {containers[i].serialNumber} ({containers[i].type}) - {containers[i].cargoWeight} kg");
                }
            }

            Console.WriteLine("\nMożliwe akcje:");
            Console.WriteLine("1. Dodaj kontenerowiec");
            Console.WriteLine("2. Usuń kontenerowiec");
            Console.WriteLine("3. Dodaj kontener");
            Console.WriteLine("4. Usuń kontener");
            Console.WriteLine("5. Załaduj kontener na statek");
            Console.WriteLine("6. Rozładuj kontener ze statku");
            Console.WriteLine("7. Przenieś kontener między statkami");
            Console.WriteLine("8. Zamień kontener na statku");
            Console.WriteLine("9. Wyświetl informacje o kontenerze");
            Console.WriteLine("10. Wyświetl informacje o statku");
            Console.WriteLine("11. Załaduj ładunek do kontenera");
            Console.WriteLine("12. Zakończ program");
            Console.Write("\nWybierz akcję: ");
        }

        static void Main(string[] args)
        {
            while (true)
            {
                DisplayMainMenu();
                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            AddContainerShip();
                            break;
                        case "2":
                            RemoveContainerShip();
                            break;
                        case "3":
                            AddContainer();
                            break;
                        case "4":
                            RemoveContainer();
                            break;
                        case "5":
                            LoadContainerToShip();
                            break;
                        case "6":
                            UnloadContainerFromShip();
                            break;
                        case "7":
                            TransferContainerBetweenShips();
                            break;
                        case "8":
                            ReplaceContainerOnShip();
                            break;
                        case "9":
                            DisplayContainerInfo();
                            break;
                        case "10":
                            DisplayShipInfo();
                            break;
                        case "11":
                            LoadCargoToContainer();
                            break;
                        case "12":
                            return; // Exit
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }
        static void AddContainerShip()
        {
            Console.Clear();
            Console.WriteLine("=== Dodawanie kontenerowca ===\n");

            Console.Write("Podaj nazwę statku: ");
            string name = Console.ReadLine();

            Console.Write("Podaj maksymalną prędkość (węzły): ");
            double maxSpeed = double.Parse(Console.ReadLine());

            Console.Write("Podaj maksymalną liczbę kontenerów: ");
            int maxContainerCount = int.Parse(Console.ReadLine());

            Console.Write("Podaj maksymalną wagę (tony): ");
            double maxWeight = double.Parse(Console.ReadLine());

            ships.Add(new ContainerShip(name, maxSpeed, maxContainerCount, maxWeight));
            Console.WriteLine("\nKontenerowiec został dodany pomyślnie!");
        }

        static void RemoveContainerShip()
        {
            if (ships.Count == 0)
            {
                Console.WriteLine("Brak dostępnych kontenerowców.");
                return;
            }

            Console.Clear();
            Console.WriteLine("=== Usuwanie kontenerowca ===\n");
            Console.WriteLine("Wybierz kontenerowiec do usunięcia:");
            for (int i = 0; i < ships.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {ships[i].name}");
            }

            int choice = int.Parse(Console.ReadLine()) - 1;
            if (choice >= 0 && choice < ships.Count)
            {
                ships.RemoveAt(choice);
                Console.WriteLine("Kontenerowiec został usunięty pomyślnie!");
            }
        }

        static void AddContainer()
        {
            Console.Clear();
            Console.WriteLine("=== Dodawanie kontenera ===\n");
            Console.WriteLine("Wybierz typ kontenera:");
            Console.WriteLine("1. Ciekły");
            Console.WriteLine("2. Gazowy");
            Console.WriteLine("3. Chłodniczy");

            int typeChoice = int.Parse(Console.ReadLine());
            Container container = null;

            switch (typeChoice)
            {
                case 1:
                    Console.Write("Czy ładunek jest niebezpieczny? (t/n): ");
                    bool isHazardous = Console.ReadLine().ToLower() == "t";
                    container = new LiquidContainer(0, 250, 250, 30000, isHazardous);
                    break;
                case 2:
                    Console.Write("Podaj ciśnienie (atm): ");
                    double pressure = double.Parse(Console.ReadLine());
                    container = new GasContainer(0, 250, 250, 25000, pressure);
                    break;
                case 3:
                    Console.Write("Podaj typ produktu (Bananas/Chocolate/Fish/Meat/Ice cream/Frozen pizza): ");
                    string productType = Console.ReadLine();
                    Console.Write("Podaj temperaturę (°C): ");
                    double temperature = double.Parse(Console.ReadLine());
                    container = new CoolingContainer(0, 250, 250, 28000, productType, temperature);
                    break;
                default:
                    Console.WriteLine("Nieprawidłowy wybór!");
                    return;
            }

            container.SetSerialNumber(containerCounter++);
            containers.Add(container);
            Console.WriteLine($"Kontener został dodany pomyślnie! Numer seryjny: {container.serialNumber}");
        }

        static void RemoveContainer()
        {
            if (containers.Count == 0)
            {
                Console.WriteLine("Brak dostępnych kontenerów.");
                return;
            }

            Console.Clear();
            Console.WriteLine("=== Usuwanie kontenera ===\n");
            Console.WriteLine("Wybierz kontener do usunięcia:");
            for (int i = 0; i < containers.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {containers[i].serialNumber} ({containers[i].type})");
            }

            int choice = int.Parse(Console.ReadLine()) - 1;
            if (choice >= 0 && choice < containers.Count)
            {
                containers.RemoveAt(choice);
                Console.WriteLine("Kontener został usunięty pomyślnie!");
            }
        }

        

        static void UnloadContainerFromShip()
        {
            if (ships.Count == 0)
            {
                Console.WriteLine("Brak dostępnych statków.");
                return;
            }

            Console.Clear();
            Console.WriteLine("=== Rozładowanie kontenera ze statku ===\n");

            Console.WriteLine("Wybierz statek:");
            for (int i = 0; i < ships.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {ships[i].name}");
            }
            int shipIndex = int.Parse(Console.ReadLine()) - 1;

            if (shipIndex >= 0 && shipIndex < ships.Count)
            {
                var ship = ships[shipIndex];
                if (ship.containers.Count == 0)
                {
                    Console.WriteLine("Statek nie ma załadowanych kontenerów.");
                    return;
                }

                Console.WriteLine("\nWybierz kontener do rozładowania:");
                for (int i = 0; i < ship.containers.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {ship.containers[i].serialNumber}");
                }
                int containerIndex = int.Parse(Console.ReadLine()) - 1;

                if (containerIndex >= 0 && containerIndex < ship.containers.Count)
                {
                    ship.containers[containerIndex].EmptyingCargo();
                    ship.containers.RemoveAt(containerIndex);
                    Console.WriteLine("Kontener został rozładowany pomyślnie!");
                }
            }
        }

        static void TransferContainerBetweenShips()
        {
            if (ships.Count < 2)
            {
                Console.WriteLine("Potrzebne są co najmniej dwa statki do transferu.");
                return;
            }

            Console.Clear();
            Console.WriteLine("=== Transfer kontenera między statkami ===\n");

            Console.WriteLine("Wybierz statek źródłowy:");
            for (int i = 0; i < ships.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {ships[i].name}");
            }
            int sourceShipIndex = int.Parse(Console.ReadLine()) - 1;

            Console.WriteLine("\nWybierz statek docelowy:");
            for (int i = 0; i < ships.Count; i++)
            {
                if (i != sourceShipIndex)
                    Console.WriteLine($"{i + 1}. {ships[i].name}");
            }
            int targetShipIndex = int.Parse(Console.ReadLine()) - 1;

            if (sourceShipIndex >= 0 && sourceShipIndex < ships.Count && targetShipIndex >= 0 && targetShipIndex < ships.Count)
            {
                var sourceShip = ships[sourceShipIndex];
                if (sourceShip.containers.Count == 0)
                {
                    Console.WriteLine("Statek źródłowy nie ma załadowanych kontenerów.");
                    return;
                }

                Console.WriteLine("\nWybierz kontener do transferu:");
                for (int i = 0; i < sourceShip.containers.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {sourceShip.containers[i].serialNumber}");
                }
                int containerIndex = int.Parse(Console.ReadLine()) - 1;

                if (containerIndex >= 0 && containerIndex < sourceShip.containers.Count)
                {
                    sourceShip.TransferContainer(sourceShip.containers[containerIndex].serialNumber, ships[targetShipIndex]);
                    Console.WriteLine("Kontener został przeniesiony pomyślnie!");
                }
            }
        }

        static void ReplaceContainerOnShip()
        {
            if (ships.Count == 0 || containers.Count == 0)
            {
                Console.WriteLine("Brak dostępnych statków lub kontenerów.");
                return;
            }

            Console.Clear();
            Console.WriteLine("=== Zamiana kontenera na statku ===\n");

            Console.WriteLine("Wybierz statek:");
            for (int i = 0; i < ships.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {ships[i].name}");
            }
            int shipIndex = int.Parse(Console.ReadLine()) - 1;

            if (shipIndex >= 0 && shipIndex < ships.Count)
            {
                var ship = ships[shipIndex];
                if (ship.containers.Count == 0)
                {
                    Console.WriteLine("Statek nie ma załadowanych kontenerów.");
                    return;
                }

                Console.WriteLine("\nWybierz kontener do zamiany:");
                for (int i = 0; i < ship.containers.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {ship.containers[i].serialNumber}");
                }
                int oldContainerIndex = int.Parse(Console.ReadLine()) - 1;

                Console.WriteLine("\nWybierz nowy kontener:");
                for (int i = 0; i < containers.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {containers[i].serialNumber}");
                }
                int newContainerIndex = int.Parse(Console.ReadLine()) - 1;

                if (oldContainerIndex >= 0 && oldContainerIndex < ship.containers.Count &&
                    newContainerIndex >= 0 && newContainerIndex < containers.Count)
                {
                    ship.ReplaceContainer(ship.containers[oldContainerIndex].serialNumber, containers[newContainerIndex]);
                    Console.WriteLine("Kontener został zamieniony pomyślnie!");
                }
            }
        }

        static void DisplayContainerInfo()
        {
            if (containers.Count == 0)
            {
                Console.WriteLine("Brak dostępnych kontenerów.");
                return;
            }

            Console.Clear();
            Console.WriteLine("=== Informacje o kontenerze ===\n");

            Console.WriteLine("Wybierz kontener:");
            for (int i = 0; i < containers.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {containers[i].serialNumber}");
            }
            int containerIndex = int.Parse(Console.ReadLine()) - 1;

            if (containerIndex >= 0 && containerIndex < containers.Count)
            {
                Console.WriteLine("\nInformacje o kontenerze:");
                Console.WriteLine($"Numer seryjny: {containers[containerIndex].serialNumber}");
                Console.WriteLine($"Typ: {containers[containerIndex].type}");
                Console.WriteLine($"Waga ładunku: {containers[containerIndex].cargoWeight} kg");
                Console.WriteLine($"Wysokość: {containers[containerIndex].height} cm");
                Console.WriteLine($"Głębokość: {containers[containerIndex].depth} cm");
                Console.WriteLine($"Pojemność: {containers[containerIndex].capacity} kg");
            }
        }

        static void DisplayShipInfo()
        {
            if (ships.Count == 0)
            {
                Console.WriteLine("Brak dostępnych statków.");
                return;
            }

            Console.Clear();
            Console.WriteLine("=== Informacje o statku ===\n");

            Console.WriteLine("Wybierz statek:");
            for (int i = 0; i < ships.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {ships[i].name}");
            }
            int shipIndex = int.Parse(Console.ReadLine()) - 1;

            if (shipIndex >= 0 && shipIndex < ships.Count)
            {
                ships[shipIndex].PrintShipInfo();
            }
        }
    }
} 