namespace AdventOfCode{
  public class HydrothermalVenture : IPuzzle{
    public void Run(){
      InputReader inputReader = new InputReader();
      var input = inputReader.GetInputAsString("HydrothermalVenture.txt");
      Dictionary<(int r, int c), int> points = new Dictionary<(int, int), int>();
      foreach(var i in input){
        var point = i.Split(" -> ");
        var left = point[0].Split(',');
        var right = point[1].Split(',');
        (int r, int c) start = (int.Parse(left[0]), int.Parse(left[1]));
        (int r, int c) end = (int.Parse(right[0]), int.Parse(right[1]));
        //vertical line
        if(start.r == end.r){
          int low = start.c < end.c ? start.c : end.c;
          int high = start.c > end.c ? start.c : end.c;
          for(int j = low; j <= high; j++){
            if(!points.ContainsKey((start.r, j))) points[(start.r, j)] = 0;
            points[(start.r, j)]++;
          }
        //horizontal line
        }else if(start.c == end.c){
          int low = start.r < end.r ? start.r : end.r;
          int high = start.r > end.r ? start.r : end.r;
          for(int j = low; j <= high; j++){
            if(!points.ContainsKey((j, start.c))) points[(j, start.c)] = 0;
            points[(j, start.c)]++;
          }
        //diagonal line
        }else{
          while(start != end){
            if(!points.ContainsKey((start.r, start.c))) points[(start.r, start.c)] = 0;
            points[(start.r, start.c)]++;
            start.r += start.r < end.r ? 1 : -1;
            start.c += start.c < end.c ? 1 : -1;
          }
          if(!points.ContainsKey((start.r, start.c))) points[(start.r, start.c)] = 0;
          points[(start.r, start.c)]++;
        }
      }
      int overlap = 0;
      foreach(var kvp in points){
        if(kvp.Value > 1) overlap++;
      }
      Console.WriteLine("Second: " + overlap);
    }
  }
}
