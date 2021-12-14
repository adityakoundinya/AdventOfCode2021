using System.Text;
namespace AdventOfCode {
  public class ExtendedPolymerization : IPuzzle {
    public void Run(){
      InputReader inputReader = new InputReader();
      var input = inputReader.GetInputAsString("ExtendedPolymerization.txt");
      string template = input[0];

      Dictionary<string, string> map = new Dictionary<string, string>();
      for(int i = 2; i < input.Count; i++){
        var temp = input[i].Split("->");
        map[temp[0].Trim()] = temp[1].Trim();
      }

      Dictionary<string, long> tempMap = new Dictionary<string, long>();
      int p = 0;
      while(p < template.Length - 1){
        var temp = template.Substring(p, 2);
        if(!tempMap.ContainsKey(temp)) tempMap[temp] = 0;
        tempMap[temp]++;
        p++;
      }
      for(int i = 0; i < 40; i++) {
        tempMap = LookUpAndSplit(tempMap, map);
        if(i == 9) BuildFreqMapAndPrintResult("First: ", tempMap);
      }
      BuildFreqMapAndPrintResult("Second: ", tempMap);
    }

    private void BuildFreqMapAndPrintResult(string message, Dictionary<string, long> tempMap){
      Dictionary<char, long> freqMap = new Dictionary<char, long>();
      foreach(var kvp in tempMap){
        if(!freqMap.ContainsKey(kvp.Key[0])) freqMap[kvp.Key[0]] = 0;
        freqMap[kvp.Key[0]] += kvp.Value;
        if(!freqMap.ContainsKey(kvp.Key[1])) freqMap[kvp.Key[1]] = 0;
        freqMap[kvp.Key[1]] += kvp.Value;
      }

      double result = Math.Ceiling((double)(freqMap.Values.Max() - freqMap.Values.Min()) / 2);
      Console.WriteLine(message + result);
    }
    private Dictionary<string, long> LookUpAndSplit(Dictionary<string, long> template, Dictionary<string, string> map){
      Dictionary<string, long> result = new Dictionary<string, long>();
      foreach(var kvp in template){
        if(map.ContainsKey(kvp.Key)){
          var first = kvp.Key[0].ToString() + map[kvp.Key];
          var second = map[kvp.Key] + kvp.Key[1].ToString();
          if(!result.ContainsKey(first)) result[first] = 0;
          result[first] += kvp.Value;
          if(!result.ContainsKey(second)) result[second] = 0;
          result[second] += kvp.Value;
        }else{
          if(!result.ContainsKey(kvp.Key)) result[kvp.Key] = 0;
          result[kvp.Key] += kvp.Value;
        }
      }
      return result;
    }
  }
}
