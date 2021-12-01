namespace AdventOfCode{
  public class SonarSweep: IPuzzle{
    public void Run(){
      var inputReader = new InputReader();
      List<int> inputLst = inputReader.GetInputAsInt("SonarSweep.txt");
      int result = 0;
      for(int i = 1; i < inputLst.Count; i++){
        if(inputLst[i - 1] < inputLst[i]) result++;
      }
      Console.WriteLine("First: " + result);
      result = 0;
      int prevSum = inputLst[0] + inputLst[1] + inputLst[2];
      for(int i = 3; i < inputLst.Count; i++){
        int a = inputLst[i - 3];
        int currSum = prevSum - a + inputLst[i];
        if(prevSum < currSum) result++;
        prevSum = currSum;
      }
      Console.WriteLine("Second: " + result);
    }
  }
}
