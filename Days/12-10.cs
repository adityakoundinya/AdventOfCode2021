namespace AdventOfCode{
  public class SyntaxScoring : IPuzzle {
    Dictionary<char, char> map = new Dictionary<char, char>();
    Dictionary<char, char> revMap = new Dictionary<char, char>();
    Dictionary<char, int> firstScore = new Dictionary<char, int>();
    Dictionary<char, int> secondScore = new Dictionary<char, int>();
    public void Run(){
      InputReader inputReader = new InputReader();
      var input = inputReader.GetInputAsString("SyntaxScoring.txt");
      InstantiateDictionaries();
      
      Dictionary<char, int> bads = new Dictionary<char, int>();
      List<long> lineScores = new List<long>();

      foreach(var line in input){
        Stack<char> st = new Stack<char>();
        bool isBad = false;
        foreach(char c in line){
          if(map.ContainsKey(c)){
            if(st.Count < 1 || st.Peek() != map[c]){
              if(!bads.ContainsKey(c)) bads[c] = 0;
              bads[c]++;
              isBad = true;
              break;
            }else{
              st.Pop();
            }
          }else{
            st.Push(c);
          }
        }
        if(isBad || st.Count < 1) continue;
        long lineScore = 0;
        while(st.Count > 0){
          char c = st.Pop();
          lineScore = (lineScore * 5) + secondScore[revMap[c]];
        }
        lineScores.Add(lineScore);
      }

      int result = 0;
      foreach(var kvp in bads){
        result += kvp.Value * firstScore[kvp.Key];
      }
      Console.WriteLine("First: " + result);

      lineScores.Sort();
      Console.WriteLine("Second: " + lineScores[lineScores.Count / 2]);
    }

    private void InstantiateDictionaries(){
      map.Add('}', '{');
      map.Add(')','(');
      map.Add(']','[');
      map.Add('>','<');
      revMap.Add('{', '}');
      revMap.Add('(',')');
      revMap.Add('[',']');
      revMap.Add('<','>');
      firstScore.Add(')',3);
      firstScore.Add(']',57);
      firstScore.Add('}',1197);
      firstScore.Add('>',25137);
      secondScore.Add(')',1);
      secondScore.Add(']',2);
      secondScore.Add('}',3);
      secondScore.Add('>',4);
    }
  }
}
