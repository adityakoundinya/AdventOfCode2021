using System.Text;

namespace AdventOfCode
{
  public class SnailFish : IPuzzle
  {
    public void Run()
    {
      InputReader inputReader = new InputReader();
      var input = inputReader.GetInputAsString("SnailFish.txt");
      string s = input[0];
      for(int i = 1; i < input.Count; i++){
        s = Add(s, input[i]);
        s = Process(s);
      }
      //Console.WriteLine("Final Sum: " + s);
      Console.WriteLine("First: " + Magnitude(s));

      long result = 0;
      for(int i = 0; i < input.Count; i++){
        for(int j = 0; j < input.Count; j++){
          if(i == j) continue;
          string twoSum = Add(input[i], input[j]);
          twoSum = Process(twoSum);
          result = Math.Max(result, Magnitude(twoSum));
        }
      }
      Console.WriteLine("Second: " + result);
    }

    private string Add(string s1, string s2)
    {
      string s = $"[{s1},{s2}]";
      //Console.WriteLine("Add : " + s);
      return s;
    }
    private string Process(string s){
      bool didExplode = false;
      bool didSplit = false;
      do{
        var exp = Explode(s);
        s = exp.s;
        didExplode = exp.didExplode;
        var split = Split(s);
        s = split.s;
        didSplit = split.didSplit;

      }while(didExplode || didSplit);
      return s;
    }
    private List<string> SplitStringToList(string s){
      List<string> st = new List<string>();
      string prev = string.Empty;
      for(int i = 0; i < s.Length; i++){
        if(!char.IsDigit(s[i])){
          if(prev != string.Empty) st.Add(prev);
          prev = string.Empty;
          st.Add(s[i].ToString());
        }else{
          prev += s[i].ToString();
        }
      }
      return st;
    }
    private (bool didSplit, string s) Split(string s){
      bool didSplit = false;
      List<string> st = SplitStringToList(s);
      for(int i = 0; i < st.Count; i++){
        if(st[i].Length == 2){
          didSplit = true;
          var regNum = Convert.ToDecimal(st[i]);
          int down = (int)Math.Floor(regNum / 2);
          int up = (int)Math.Ceiling(regNum / 2);
          st[i] = $"[{down},{up}]";
          break;
        }
      }
      string sSplit = string.Join(string.Empty, st);
      //Console.WriteLine("Split: " + sSplit);
      return (didSplit, sSplit);
    }
    private (bool didExplode, string s) Explode(string s1)
    {
      List<string> st = new List<string>();
      bool didExplode = false;
      bool done = false;
      do
      {
        int nests = 0;
        int insertIdx = 0;
        int prevAdd = -1;
        int afterAdd = -1;
        int prev = -1;
        done = false;
        for (int i = 0; i < s1.Length; i++)
        {
          if (char.IsDigit(s1[i]))
          {
            if (prev == -1)
            {
              prev = Convert.ToInt32(s1[i].ToString());
              st.Add(prev.ToString());
            }
            else
            {
              prev = prev * 10;
              prev += Convert.ToInt32(s1[i].ToString());
              st.RemoveAt(st.Count - 1);
              st.Add(prev.ToString());
            }
          }
          else if (s1[i] == '[')
          {
            prev = -1;
            if (nests == 4 && !done)
            {
              done = true;
              st.Add("0");
              insertIdx = st.Count - 1;
              i++;
              string temp = string.Empty;
              while (s1[i] != ',')
              {
                temp += s1[i].ToString();
                i++;
              }
              prevAdd = Convert.ToInt32(temp);
              i++;
              temp = string.Empty;
              while (s1[i] != ']')
              {
                temp += s1[i].ToString();
                i++;
              }
              afterAdd = Convert.ToInt32(temp);
            }
            else
            {
              nests++;
              st.Add(s1[i].ToString());
            }
          }
          else
          {
            if (s1[i] == ']') nests--;
            prev = -1;
            st.Add(s1[i].ToString());
          }
        }
        if (done)
        {
          didExplode = true;
          int i = insertIdx - 1;
          while (i >= 0)
          {
            if (int.TryParse(st[i], out var temp))
            {
              temp += prevAdd;
              st[i] = temp.ToString();
              break;
            }
            else
            {
              i--;
            }
          }
          i = insertIdx + 1;
          while (i < st.Count)
          {
            if (int.TryParse(st[i], out var temp))
            {
              temp += afterAdd;
              st[i] = temp.ToString();
              break;
            }
            else
            {
              i++;
            }
          }
          s1 = string.Join(string.Empty, st);
          st = new List<string>();
          //Console.WriteLine("Explode: " + s1);
        }
      } while (done);
      return (didExplode, s1);
    }
    private long Magnitude(string s){
      List<string> lstStr = SplitStringToList(s);
      Stack<string> st = new Stack<string>();
      foreach(var i in lstStr){
        if(i == ",") continue;
        if(i != "]"){
          st.Push(i);
        }else{
          string a = st.Pop();
          string b = st.Pop();
          st.Pop();
          long temp = (3 * long.Parse(b)) + (2 * long.Parse(a));
          st.Push(temp.ToString());
        }
      }
      return long.Parse(string.Join(string.Empty, st));
    }
  }
}
