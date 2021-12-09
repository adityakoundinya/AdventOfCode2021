namespace AdventOfCode{
  public class SevenSegmentSearch : IPuzzle{
    public void Run(){
      InputReader inputReader = new InputReader();
      var input = inputReader.GetInputAsString("SevenSegmentSearch.txt");
      int result = 0;
      foreach(var i in input){
        var numbers = i.Split('|')[1].Split(' ');
        foreach(var n in numbers){
          int len = n.Length;
          if(len == 2 || len == 4 || len == 3 || len == 7) result++;
        }
      }
      Console.WriteLine("First: " + result);

      result = 0;
      List<int> numList = new List<int>();
      foreach(var i in input){
        var temp = i.Split('|');
        var guess = temp[0].Trim().Split(' ').ToList().OrderBy(o => o.Length);
        var num = temp[1].Trim().Split(' ');
        Dictionary<int, string> revMap = new Dictionary<int, string>();
        Dictionary<string, int> map = new Dictionary<string, int>();
        foreach(var s in guess){
          var ordStr = String.Concat(s.OrderBy(c => c));
          switch(s.Length){
            case 2:
              map[ordStr] = 1;
              revMap[1] = ordStr;
              break;
            case 3:
              map[ordStr] = 7;
              revMap[7] = ordStr;
              break;
            case 4:
              map[ordStr] = 4;
              revMap[4] = ordStr;
              break;
            case 7:
              map[ordStr] = 8;
              revMap[8] = ordStr;
              break;
            case 5:
              int commonWith7 = CountCharsCommon(ordStr, revMap[7]);
              int commonWith4 = CountCharsCommon(ordStr, revMap[4]);
              if(commonWith7 == 3) map[ordStr] = 3;
              else{
                if(commonWith4 == 3) map[ordStr] = 5;
                else map[ordStr] = 2;
              }
              break;
            case 6:
              commonWith4 = CountCharsCommon(ordStr, revMap[4]);
              commonWith7 = CountCharsCommon(ordStr, revMap[7]);
              if(commonWith4 == 4) map[ordStr] = 9;
              else if(commonWith7 == 3) map[ordStr] = 0;
              else map[ordStr] = 6;
              break;
          }
        }
        int number = 0;
        foreach(var j in num){
          var ordStr = String.Concat(j.OrderBy(c => c));
          number = number * 10 + map[ordStr];
        }
        numList.Add(number);
        result += number;
      }
      //Console.WriteLine(string.Join(',', numList));
      Console.WriteLine("Second: " + result);
    }
    private int CountCharsCommon(string str1, string str2){
      int count = 0;
      foreach(char i in str1){
        if(str2.Contains(i)) count++;
      }
      return count;
    }
  }
}
