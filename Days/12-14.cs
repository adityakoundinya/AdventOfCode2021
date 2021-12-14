using System.Text;
namespace AdventOfCode {
  public class ExtendedPolymerization : IPuzzle {
    public void Run(){
      InputReader inputReader = new InputReader();
      var input = inputReader.GetInputAsString("test.txt");
      string template = input[0];
      Dictionary<string, long> tempMap = new Dictionary<string, long>();
      int p = 0;
      while(p < template.Length - 1){
        var temp = template.Substring(p, 2);
        if(!tempMap.ContainsKey(temp)) tempMap[temp] = 0;
        tempMap[temp]++;
        p++;
      }
      Dictionary<string, string> map = new Dictionary<string, string>();
      for(int i = 2; i < input.Count; i++){
        var temp = input[i].Split("->");
        map[temp[0].Trim()] = temp[1].Trim();
      }
      int step = 0;
      while(step < 40){
        // StringBuilder sb = new StringBuilder();
        // int i = 0;
        // int j = 1;
        // sb.Append(template[i].ToString());
        // while(j < template.Length){
        //   var temp = template[i].ToString() + template[j].ToString();
        //   if(map.ContainsKey(temp)){
        //     sb.Append(map[temp] + template[j].ToString());
        //   }else{
        //     sb.Append(template[j].ToString());
        //   }
        //   i++;
        //   j++;
        // }
        // template = sb.ToString();
        tempMap = LookUpAndSplit(tempMap, map);
        step++;
      }
      // Dictionary<char, long> freqMap = new Dictionary<char, long>();
      // foreach(var c in template){
      //   if(!freqMap.ContainsKey(c)) freqMap[c] = 0;
      //   freqMap[c]++;
      // }
      // double result = freqMap.Values.Max() - freqMap.Values.Min();
      // Console.WriteLine("First: " + result);

      Dictionary<char, long> freqMap2 = new Dictionary<char, long>();
      foreach(var kvp in tempMap){
        if(!freqMap2.ContainsKey(kvp.Key[0])) freqMap2[kvp.Key[0]] = 0;
        freqMap2[kvp.Key[0]] += kvp.Value;
        if(!freqMap2.ContainsKey(kvp.Key[1])) freqMap2[kvp.Key[1]] = 0;
        freqMap2[kvp.Key[1]] += kvp.Value;
      }

      double result = Math.Ceiling((double)(freqMap2.Values.Max() - freqMap2.Values.Min()) / 2);
      Console.WriteLine("Compare: " + result);
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
