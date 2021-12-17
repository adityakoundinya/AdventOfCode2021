namespace AdventOfCode{
  public class TrickShot : IPuzzle {
    int x1 = 0;
    int x2 = 0;
    int y1 = 0;
    int y2 = 0;
    public void Run(){
      InputReader inputReader = new InputReader();
      var input = inputReader.GetInputAsString("TrickShot.txt");

      var range = input[0].Split(':')[1].Trim();
      var xRange = range.Split(',')[0].Trim();
      var yRange = range.Split(',')[1].Trim();
      var xStr = xRange.Split('=')[1].Split("..");
      x1 = int.Parse(xStr[0]);
      x2 = int.Parse(xStr[1]);
      var yStr = yRange.Split('=')[1].Split("..");
      y1 = int.Parse(yStr[1]);
      y2 = int.Parse(yStr[0]);

      long maxY = (y2 * (y2 +  1)) / 2;
      Console.WriteLine("First: " + maxY);

      int result = 0;
      for(int x = 0; x <= x2; x++){
        for(int y = y2; y <= maxY; y++){
          if(IsHit(x, y)) result++;
        }
      }
      Console.WriteLine("Second: " + result);
    }

    private bool IsHit(int x, int y){
      int drag = x - 1;
      int gravity = y - 1;
      while(!IsOutOfRange(x, y)){
        if(IsInRange(x, y)) return true;
        x += drag == 0 ? 0 : drag--;
        y += gravity--;
      }
      return false;
    }

    private bool IsInRange(int x, int y){
      return x1 <= x && x <= x2 && y2 <= y && y <= y1;
    }

    private bool IsOutOfRange(int x, int y){
      return x > x2 || y < y2;
    }
  }
}
