namespace AdventOfCode{
  public class Lanternfish : IPuzzle{
    public void Run(){
      InputReader inputReader = new InputReader();
      var input = inputReader.GetInputAsIntFromCsv("Lanternfish.txt");
      Dictionary<int, long> fish = new Dictionary<int, long>();
      for(int i = -1; i < 9; i++) fish[i] = 0;
      foreach(var i in input) fish[i]++;
      for(int i = 0; i < 256; i++){
        Count(fish);
      }
      long result = 0;
      foreach(var kvp in fish) result += kvp.Value;
      Console.WriteLine("Second: " + result);
    }

    private void Count(Dictionary<int, long> fish){
      foreach(var kvp in fish){
        if(kvp.Key == -1) continue;
        fish[kvp.Key - 1] += fish[kvp.Key];
        fish[kvp.Key] = 0;
      }
      long birth = fish[-1];
      fish[8] += birth;
      fish[6] += birth;
      fish[-1] = 0;
    }
  }
}
