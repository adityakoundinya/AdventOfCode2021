using System.Text;
namespace AdventOfCode{
  public class BinaryDiagnostic: IPuzzle{

    public void Run(){
      InputReader inputReader = new InputReader();
      var input = inputReader.GetInputAsString("BinaryDiagnostic.txt");

      List<int> ones = new List<int>();
      List<int> zeros = new List<int>();
      foreach(var num in input){
        for(int i = 0; i < num.Length; i++){
          if(i >= ones.Count){
            ones.Add(0);
            zeros.Add(0);
          }
          if(num[i] == '0') zeros[i]++;
          else ones[i]++;
        }
      }

      StringBuilder gamma = new StringBuilder();
      StringBuilder epsilon = new StringBuilder();
      for(int i = 0; i < ones.Count; i++){
        if(ones[i] >= zeros[i]){
          gamma.Append("1");
          epsilon.Append("0");
        }else{
          gamma.Append("0");
          epsilon.Append("1");
        }
      }

      int g = Convert.ToInt32(gamma.ToString(), 2);
      int e = Convert.ToInt32(epsilon.ToString(), 2);

      Console.WriteLine("First: " + g*e);

      int n = input[0].Length;

      List<string> filtered = new List<string>(input);
      for(int i = 0; i < n; i++){
        if(filtered.Count <= 1) break;
        bool isOnes = isOnesGreater(filtered, i);
        filtered = Filter(filtered, isOnes ? '1' : '0', i);
      }
      int o2 = Convert.ToInt32(filtered[0], 2);

      filtered = new List<string>(input);
      for(int i = 0; i < n; i++){
        if(filtered.Count <= 1) break;
        bool isOnes = isOnesGreater(filtered, i);
        filtered = Filter(filtered, isOnes ? '0' : '1', i);
      }
      int co2 = Convert.ToInt32(filtered[0], 2);

      Console.WriteLine("Second: " + o2 * co2);
    }

    private bool isOnesGreater(List<string> input, int idx){
      int ones = 0;
      int zeros = 0;
      foreach(var num in input){
        if(num[idx] == '1') ones++;
        else zeros++;
      }
      return ones >= zeros;
    }

    private List<string> Filter(List<string> input, char filterChar, int idx){
      List<string> result = new List<string>();
      foreach(var i in input){
        if(i[idx] == filterChar) result.Add(i);
      }
      return result;
    }
  }
}
