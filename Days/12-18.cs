using System.Text;

namespace AdventOfCode
{
  public class SnailFish : IPuzzle
  {
    public void Run()
    {
      InputReader inputReader = new InputReader();
      var input = inputReader.GetInputAsString("test.txt");
      string s = input[0];
      for(int i = 1; i < input.Count; i++){
        s = Add(s, input[i]);
        s = Process(s);
      }
      Console.WriteLine("Final Sum: " + s);
      Console.WriteLine("First: " + Magnitude(s));
    }

    private string Add(string s1, string s2)
    {
      return $"[{s1},{s2}]";
    }
    private string Process(string s){
      int len = 0;
      do{
        len = s.Length;
        s = Explode(s);
        s = Split(s);
      }while(len != s.Length);
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
    private string Split(string s){
      List<string> st = SplitStringToList(s);
      for(int i = 0; i < st.Count; i++){
        if(st[i].Length == 2){
          var regNum = Convert.ToDecimal(st[i]);
          int down = (int)Math.Floor(regNum / 2);
          int up = (int)Math.Ceiling(regNum / 2);
          st[i] = $"[{down},{up}]";
          break;
        }
      }
      return string.Join(string.Empty, st);
    }
    private string Explode(string s1)
    {
      List<string> st = new List<string>();
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
        }
      } while (done);
      return string.Join(string.Empty, st);
    }
    private string Magnitude(string s){
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
      return string.Join(string.Empty, st);
    }
  }
}
