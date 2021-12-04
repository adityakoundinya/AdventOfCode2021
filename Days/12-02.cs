namespace AdventOfCode{
  public class Dive: IPuzzle {
    public void Run(){
      InputReader inputReader = new InputReader();
      List<string> input = inputReader.GetInputAsString("Dive.txt");
      long x = 0;
      long y = 0;
      foreach(var i in input){
        string move = i.Split(" ")[0];
        int len  = int.Parse(i.Split(" ")[1]);
        switch(move){
          case "forward":
            x += len;
            break;
          case "down":
            y += len;
            break;
          default:
            y -= len;
            break;
        }
      }
      Console.WriteLine("First Position: " + x * y);

      x = 0;
      y = 0;
      long aim = 0;
      foreach(var i in input){
        string move = i.Split(" ")[0];
        int len  = int.Parse(i.Split(" ")[1]);
        switch(move){
          case "forward":
            x += len;
            y += aim * len;
            break;
          case "down":
            aim += len;
            break;
          default:
            aim -= len;
            break;
        }
      }
      Console.WriteLine("Second Position: " + x * y);
    }
  }
}
