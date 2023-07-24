using System;
using System.Xml.Linq;

namespace iA_CodingChallenge
{
    class ClosestCenterFills
    {
        public static void Main()
        {
            int xMin = -10;
            int xMax = 10;
            int yMin = -10;
            int yMax = 10;

            Random random = new Random();
            int numOfCenters = random.Next(1, ((xMax - xMin) * (yMax - yMin)));
            Console.WriteLine(numOfCenters);
            List<Location> locations = GenerateRandomLocations(numOfCenters, xMin, xMax, yMin, yMax);

            List<CentralFillFacility> centers = new List<CentralFillFacility>();
            int lastId = 0;
            foreach (var location in locations)
            {
                lastId++;
                centers.Add(new CentralFillFacility(location, lastId.ToString("D3")));
                //Console.WriteLine(location.xLoc + ", " + location.yLoc);
            }

            Console.WriteLine("Please Input Coordinates: (whole numbers only)");
            string userInput = Console.ReadLine();
            Location userCoordinate = new Location(int.Parse(userInput.Split(',')[0]), int.Parse(userInput.Split(',')[1]));
                        
            List<KeyValuePair<CentralFillFacility, int>> shortestDistancesToFacility = new List<KeyValuePair<CentralFillFacility, int>>();
            KeyValuePair<CentralFillFacility, int> maxShortestDistance = new KeyValuePair<CentralFillFacility, int>();

            foreach (var facility in centers)
            {
                int distance = getManhattanDistance(userCoordinate, facility.centerLocation);
                if (shortestDistancesToFacility.Count < 3)
                {
                    shortestDistancesToFacility.Add(new KeyValuePair<CentralFillFacility, int>(facility, distance));
                    maxShortestDistance = shortestDistancesToFacility.OrderByDescending(kvp => kvp.Value).First();
                }
                else if (distance <= maxShortestDistance.Value)
                {
                    //Only replace the facility if they are equal and the center we are checking has a cheaper meidcation price
                    //OR if the distnace is shorter
                    if (distance < maxShortestDistance.Value
                        ||(distance == maxShortestDistance.Value && maxShortestDistance.Key.getCheapestMedication().Cost >= facility.getCheapestMedication().Cost))
                    {
                        shortestDistancesToFacility.Remove(maxShortestDistance);
                        shortestDistancesToFacility.Add(new KeyValuePair<CentralFillFacility, int>(facility, distance));
                        maxShortestDistance = shortestDistancesToFacility.OrderByDescending(kvp => kvp.Value).First();
                    }
                    
                }
            }

            shortestDistancesToFacility.Sort((kvp1, kvp2) => kvp1.Value.CompareTo(kvp2.Value));

            Console.WriteLine();
            Console.WriteLine("Closest Central Fills to (" + userInput + ")");
            foreach (var facilityKvp in shortestDistancesToFacility)
            {
                CentralFillFacility facility = facilityKvp.Key;
                Medication cheapestMed = facility.getCheapestMedication();
                Console.WriteLine("Central Fill " + facility.uniqueId + " - " + cheapestMed.Cost.ToString("C2") + ", Medication " + cheapestMed.Name + ", Distance " + facilityKvp.Value);
            }
            
        }

        public static List<Location> GenerateRandomLocations(int count, int xMin, int xMax, int yMin, int yMax)
        {
            List<Location> locations = new List<Location>();
            Random random = new Random();

            while (locations.Count < count)
            {
                int x = random.Next(xMin, xMax + 1);
                int y = random.Next(yMin, yMax + 1);

                // Check if the location already exists in the list
                bool locationExists = locations.Exists(loc => loc.xLoc == x && loc.yLoc == y);

                if (!locationExists)
                {
                    locations.Add(new Location(x, y));
                }
            }

            return locations;
        }

        public static int getManhattanDistance(Location userLoc, Location centerLoc)
        {
            return Math.Abs(centerLoc.xLoc - userLoc.xLoc) + Math.Abs(centerLoc.yLoc - userLoc.yLoc); ;
        }
    }
}