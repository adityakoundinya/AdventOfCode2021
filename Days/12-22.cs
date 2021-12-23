namespace AdventOfCode {
  public class ReactorReboot : IPuzzle {
    public void Run() {
      InputReader inputReader = new InputReader();
      var input = inputReader.GetInputAsString("ReactorReboot.txt");
      HashSet<(int x, int y, int z)> lights = new();
      List<Cube> cubes = new List<Cube>();
      bool isOn = false;
      foreach(var i in input){
        var line  = i.Split(" ");
        isOn = line[0] == "on";
        var coord = line[1].Split(",");
        int minX = 0;
        int maxX = 0;
        int minY = 0;
        int maxY = 0;
        int minZ = 0;
        int maxZ = 0;
        var xRange = coord[0].Split("=")[1].Split("..");
        minX = int.Parse(xRange[0]);
        maxX = int.Parse(xRange[1]);
        var yRange = coord[1].Split("=")[1].Split("..");
        minY = int.Parse(yRange[0]);
        maxY = int.Parse(yRange[1]);
        var zRange = coord[2].Split("=")[1].Split("..");
        minZ = int.Parse(zRange[0]);
        maxZ = int.Parse(zRange[1]);
        cubes.Add(new Cube(isOn, minX, maxX, minY, maxY, minZ, maxZ));
        for(int x = minX; x <= maxX; x++){
           if(x < -50 || x > 50) continue;
          for(int y = minY; y <= maxY; y++){
             if(y < -50 || y > 50) continue;
            for(int z = minZ; z <= maxZ; z++){
               if(z < -50 || z > 50) continue;
              if(isOn) lights.Add((x,y,z));
              else lights.Remove((x,y,z));
            }
          }
        }
      }
      Console.WriteLine("First: " + lights.Count);

      // Setup a list to hold all the cubes from the various instersections we will perform
      List<Cube> intersectedCubes = new List<Cube>();

      // cycle through each cube in our main cube list
      foreach(Cube currentCube in cubes) {
          List<Cube> intersectedCubesToAdd = new List<Cube>();
          // if the cube is on, add it to the add list.
          // it hasn't been intersected, but it will be in the next pass
          if (currentCube.On) {
              intersectedCubesToAdd.Add(currentCube);
          }
          // check the current cube against all previously intersected cubes and
          // if there's an intersect, add that resulting cube to the list to add
          foreach(Cube previouslyIntersectedCube in intersectedCubes) {
              // we send the opposite value of ON for the intersection so we don't double count on/on || off/off cubes that overlap
              Cube? newlyIntersectedCube = IntersectCubes(currentCube, previouslyIntersectedCube, !previouslyIntersectedCube.On);
              if(newlyIntersectedCube != null) {
                  intersectedCubesToAdd.Add(newlyIntersectedCube);
              }
          }
          // add all the cubes in the ToAdd list to the intersectedCubes list
          foreach(Cube c in intersectedCubesToAdd) {
              intersectedCubes.Add(c);
          }
      }

      // total up the volumes of each cube in the intersectedCubes list.
      // since adding a new cube calculates its volume we just need to cycle through them
      // any cube that is on adds volume, and off subtracts volume.
      long part2 = 0;
      foreach(Cube c in intersectedCubes) {
          if (c.On) {
              part2 += c.Volume;
          }
          else {
              part2 -= c.Volume;
          }
      }
      Console.WriteLine("Second: {0}", part2);
    }
    public Cube? IntersectCubes(Cube current, Cube previouslyIntersected, bool on) {
            // If there's no intersection, we return null as the cubes don't overlap.
            if(current.XMin > previouslyIntersected.XMax || current.XMax < previouslyIntersected.XMin ||
                current.YMin > previouslyIntersected.YMax || current.YMax < previouslyIntersected.YMin ||
                current.ZMin > previouslyIntersected.ZMax || current.ZMax < previouslyIntersected.ZMin) {
                return null;
            }
            // Otherwise we return a new cube that describes the overlap
            else {
                return new Cube(on,
                Math.Max(current.XMin, previouslyIntersected.XMin),
                Math.Min(current.XMax, previouslyIntersected.XMax),
                Math.Max(current.YMin, previouslyIntersected.YMin),
                Math.Min(current.YMax, previouslyIntersected.YMax),
                Math.Max(current.ZMin, previouslyIntersected.ZMin),
                Math.Min(current.ZMax, previouslyIntersected.ZMax));
            }
        }
  }
  public class Cube {
            public bool On { get; set; }
            public long XMin { get; set; }
            public long XMax { get; set; }
            public long YMin { get; set; }
            public long YMax { get; set; }
            public long ZMin { get; set; }
            public long ZMax { get; set; }

            public long Volume { get; set; }

            public Cube(bool on, long xMin, long xMax, long yMin, long yMax, long zMin, long zMax) {
                On = on;
                XMin = xMin;
                XMax = xMax;
                YMin = yMin;
                YMax = yMax;
                ZMin = zMin;
                ZMax = zMax;

                // adding one to each axis as this subtraction isn't inclusive
                Volume = (Math.Abs(XMax - XMin + 1) * Math.Abs(YMax - YMin + 1) * Math.Abs(ZMax - ZMin + 1));
            }
        }
}
