namespace AdventOfCode {
  public class SeaCucumbers : IPuzzle {
    public void Run(){
      InputReader inputReader = new InputReader();
      var input = inputReader.GetInputAsString("SeaCucumbers.txt");
      List<List<char>> map = new List<List<char>>();
      List<char> m;
      foreach(var i in input){
        m = new List<char>();
        m = i.ToCharArray().ToList();
        map.Add(m);
      }
      bool didMoveEast = false;
      bool didMoveSouth = false;
      int result = 0;
      do{
        result++;
        var east = MoveEast(map);
        didMoveEast = east.didMove;
        map = east.map;
        var south = MoveSouth(map);
        didMoveSouth = south.didMove;
        map = south.map;
      }while(didMoveEast || didMoveSouth);
      Console.WriteLine("First: " + result);
    }
    private (bool didMove, List<List<char>> map) MoveEast(List<List<char>> map){
      bool didMove = false;
      var newMap = CopyMap(map);
      for(int i = 0; i < map.Count; i++){
        for(int j = 0; j < map[i].Count; j++){
          if(map[i][j] != '>') continue;
          if(!CanMove(map, i, j, true)) continue;
          didMove = true;
          var nextPos = GetNextPosition(map, i, j, true);
          newMap[i][j] = '.';
          newMap[nextPos.r][nextPos.c] = '>';
        }
      }
      return (didMove, newMap);
    }

    private (bool didMove, List<List<char>> map) MoveSouth(List<List<char>> map){
      bool didMove = false;
      var newMap = CopyMap(map);
      for(int i = 0; i < map.Count; i++){
        for(int j = 0; j < map[i].Count; j++){
          if(map[i][j] != 'v') continue;
          if(!CanMove(map, i, j, false)) continue;
          didMove = true;
          var nextPos = GetNextPosition(map, i, j, false);
          newMap[i][j] = '.';
          newMap[nextPos.r][nextPos.c] = 'v';
        }
      }
      return (didMove, newMap);
    }
    private bool CanMove(List<List<char>> map, int r, int c, bool isEast){
      var nextPos = GetNextPosition(map, r, c, isEast);
      return map[nextPos.r][nextPos.c] == '.';
    }

    private(int r, int c) GetNextPosition(List<List<char>> map, int r, int c, bool isEast){
      if(isEast){
        c++;
        c %= map[r].Count;
      }else{
        r++;
        r %= map.Count;
      }
      return (r, c);
    }

    private void PrintMap(List<List<char>> map){
      foreach(var i in map){
        Console.WriteLine(new string(i.ToArray()));
      }
      Console.WriteLine("----------------------------------");
    }

    private List<List<char>> CopyMap(List<List<char>> map){
      List<List<char>> result = new List<List<char>>();
      List<char> m;
      for(int i = 0; i < map.Count; i++){
        m = new List<char>();
        for(int j = 0; j < map[i].Count; j++){
          m.Add(map[i][j]);
        }
        result.Add(m);
      }
      return result;
    }
  }
}
