namespace iA_CodingChallenge
{
    class ClosestCenterFills
    {
        public static void Main()
        {
            //This determines the grid size. It can be changes to be user input.
            int xMin = -10;
            int xMax = 10;
            int yMin = -10;
            int yMax = 10;

            //A rondom number is generated to determine the number of central fill facilities in the world
            Random random = new Random();
            int numOfCenters = random.Next(1, ((xMax - xMin) * (yMax - yMin)));
            Console.WriteLine("There are " + +numOfCenters + " fill center facilities.");
            List<Location> locations = GenerateRandomLocations(numOfCenters, xMin, xMax, yMin, yMax);

            //Create the central fill facilities at the randomly generated locations, give them a uniqueid
            List<CentralFillFacility> centers = new List<CentralFillFacility>();
            int lastId = 0;
            foreach (var location in locations)
            {
                lastId++;
                centers.Add(new CentralFillFacility(location, lastId.ToString("D3")));
            }            
            
            Console.WriteLine("Please Input Coordinates or 'DONE' to exit: (whole numbers only)");
            string userInput = Console.ReadLine();
            //The user can continue to use the same randonmly seeded map to find the 3 closest facilites until they are done
            while (userInput != "DONE")
            {
                Location userCoordinate = new Location(int.Parse(userInput.Split(',')[0]), int.Parse(userInput.Split(',')[1]));

                //Check to make sure the coordinates entered are within the map
                if (userCoordinate.xLoc >= xMin && userCoordinate.xLoc <= xMax && userCoordinate.yLoc >= yMin && userCoordinate.yLoc <= yMax)
                {
                    //List of 3 shortest facilities
                    List<KeyValuePair<CentralFillFacility, int>> shortestDistancesToFacility = new List<KeyValuePair<CentralFillFacility, int>>();

                    //This contains the longest distance, and its cooresponding facility, in the list of 3 shortest distances to use as a comparison
                    KeyValuePair<CentralFillFacility, int> maxShortestDistance = new KeyValuePair<CentralFillFacility, int>();

                    foreach (var facility in centers)
                    {
                        int distance = getManhattanDistance(userCoordinate, facility.centerLocation);
                        //Add the facility to the list if there are less than 3 in the list
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
                                || (distance == maxShortestDistance.Value && maxShortestDistance.Key.getCheapestMedication().Cost >= facility.getCheapestMedication().Cost))
                            {
                                shortestDistancesToFacility.Remove(maxShortestDistance);
                                shortestDistancesToFacility.Add(new KeyValuePair<CentralFillFacility, int>(facility, distance));
                                maxShortestDistance = shortestDistancesToFacility.OrderByDescending(kvp => kvp.Value).First();
                            }
                        }
                    }
                    
                    shortestDistancesToFacility.Sort((kvp1, kvp2) => kvp1.Value.CompareTo(kvp2.Value));
                    Console.WriteLine("\nClosest Central Fills to (" + userInput + ")");
                    foreach (var facilityKvp in shortestDistancesToFacility)
                    {
                        CentralFillFacility facility = facilityKvp.Key;
                        Medication cheapestMed = facility.getCheapestMedication();
                        Console.WriteLine("Central Fill " + facility.uniqueId + " - " + cheapestMed.Cost.ToString("C2") + ", Medication " + cheapestMed.Name + ", Distance " + facilityKvp.Value);
                    }
                }
                else
                {
                    Console.WriteLine("Please make sure coordinates are within the grid. X axis is " + xMin + " to " + xMax + ". Y axis is " + yMin + " to " + yMax);
                }

                Console.WriteLine("\nPlease Input Coordinates or 'DONE' to exit: (whole numbers only)");
                userInput = Console.ReadLine();
            }            
        }

        //Returns a list of randomly generated locations based on the grid min/max passed in and the number of location to generated
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
