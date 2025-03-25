# Container Ship Management System

A console-based application for managing container ships and their cargo containers. The system allows users to create and manage different types of containers, load them onto ships, and perform various container operations.

## Features

### Container Types
- **Liquid Container**
  - Supports hazardous and non-hazardous cargo
  - Maximum capacity: 30,000 kg
  - Hazardous cargo limited to 50% capacity
  - Non-hazardous cargo limited to 90% capacity

- **Gas Container**
  - Configurable pressure settings
  - Maximum capacity: 25,000 kg
  - Maintains 5% residual gas after unloading
  - Pressure monitoring

- **Cooling Container**
  - Temperature-controlled storage
  - Maximum capacity: 28,000 kg
  - Supports specific product types:
    - Bananas (min 13.3°C)
    - Chocolate (min 18°C)
    - Fish (min 2°C)
    - Meat (min -15°C)
    - Ice cream (min -18°C)
    - Frozen pizza (min -30°C)

### Ship Management
- Create container ships with specified parameters:
  - Maximum speed (in knots)
  - Maximum container capacity
  - Maximum total weight capacity (in tons)
- Track containers loaded on each ship
- Monitor ship's current weight and container count

### Container Operations
- Create new containers of different types
- Load cargo into containers
- Load containers onto ships
- Unload containers from ships
- Transfer containers between ships
- Replace containers on ships
- Display container information
- Display ship information and cargo

## Usage

### Main Menu Options
1. **Dodaj kontener** (Add Container)
   - Select container type
   - Configure type-specific parameters
   - Set cargo weight
   - Get container serial number

2. **Wyświetl kontenery** (Display Containers)
   - View all containers
   - See container details:
     - Serial number
     - Type
     - Capacity
     - Current cargo weight

3. **Ładowanie kontenera na statek** (Load Container to Ship)
   - Select target ship
   - Choose container to load
   - Set cargo weight
   - System validates:
     - Container capacity
     - Ship capacity
     - Weight limits
     - Container availability

4. **Wyjdź** (Exit)
   - Close the application

### Safety Features
- Prevents overloading containers
- Validates temperature requirements for cooling containers
- Ensures containers can only be on one ship at a time
- Monitors ship weight and container limits
- Handles hazardous cargo restrictions

## Technical Requirements
- .NET Framework or .NET Core
- C# programming language
- Console application environment

## Error Handling
The system includes comprehensive error handling for:
- Invalid input data
- Container capacity limits
- Ship capacity limits
- Temperature requirements
- Container availability
- Weight restrictions

## Future Improvements
- Data persistence
- User authentication
- Ship route planning
- Container tracking history
- Export/import functionality
- GUI interface 