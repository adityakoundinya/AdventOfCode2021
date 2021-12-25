namespace AdventOfCode {
    public class ALU : IPuzzle {
      public void Run(){
        InputReader inputReader = new InputReader();
        var input = inputReader.GetInputAsString("ALU.txt");
        List<int> result = new List<int>();
        List<List<string>> instructions = new List<List<string>>();
        List<string> ins = new List<string>();
        for(int i = 1; i < input.Count; i++){
          if(!input[i].StartsWith("inp")){
            ins.Add(input[i]);
          }else{
            instructions.Add(ins);
            ins = new List<string>();
          }
        }
        instructions.Add(ins);
        Dictionary<int, List<int>> map = new Dictionary<int, List<int>>();
        int place = 1;
        int z = 0;
        foreach(var i in instructions){
          map[place] = new List<int>();
          for(int j = 9; j > 0; j--){
            z = IsValid(j, z, i);
            if(z == 0){
              map[place].Add(j);
            }
          }
          place++;
        }
        Console.WriteLine("First: " + string.Join("", result));
      }
      private int IsValid(int w, int z, List<string> ins){
        int x = 0, y = 0, b = 0;
        foreach(var i in ins){
          var cal = i.Split(' ');
          switch(cal[1]){
            case "w":
              b = GetValue(w, x,y,z,cal[2]);
              w = Operate(cal[0], w, b);
            break;
            case "x":
              b = GetValue(w, x,y,z,cal[2]);
              x = Operate(cal[0], x, b);
            break;
            case "y":
              b = GetValue(w, x,y,z,cal[2]);
              y = Operate(cal[0], y, b);
            break;
            case "z":
              b = GetValue(w, x,y,z,cal[2]);
              z = Operate(cal[0], z, b);
            break;
          }
        }
        return z;
      }
      private int Operate(string o, int a, int b) => o switch
      {
        "add" => a + b,
        "mul" => a * b,
        "div" => a / b,
        "mod" => a % b,
        "eq" => a == b ? 1 : 0,
        _ => 0
      };
      private int GetValue(int w, int x, int y, int z, string variable) => variable switch{
        "x" => x,
        "y" => y,
        "z" => z,
        "w" => w,
        _ => int.Parse(variable)
      };
    }
}
