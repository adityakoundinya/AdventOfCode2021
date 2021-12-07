namespace AdventOfCode{
  public class WhaleTreachery : IPuzzle{
    public void Run(){
      InputReader inputReader = new InputReader();
      var input = inputReader.GetInputAsIntFromCsv("WhaleTreachery.txt");
      int low = int.MaxValue;
      int high = 0;
      foreach(int i in input){
        low = Math.Min(low, i);
        high = Math.Max(high, i);
      }
      int result = int.MaxValue;
      for(int i = low; i <= high; i++){
        int temp = 0;
        foreach(var n in input){
          temp += Math.Abs(i - n);
        }
        result = Math.Min(result, temp);
      }
      Console.WriteLine("First: " + result);
    }
  }
}
