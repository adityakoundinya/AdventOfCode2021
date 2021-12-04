namespace AdventOfCode{
  public class GiantSquid : IPuzzle{
    public void Run(){
      InputReader inputReader = new InputReader();
      var draws = inputReader.GetDrawingInputForGiantSquid("GiantSquid.txt");
      var boards = inputReader.GetBoardInputForGiantSquid("GiantSquid.txt");
      var locations = BuildNumLocation(boards);
      int result = 0;
      foreach(var d in draws){
        var locs = locations[d];
        foreach(var loc in locs){
          var board = boards[loc.BoardId];
          if(Check(board, loc)){
            result = CalculateAnswer(board, d);
            Console.WriteLine("First: "+ result);
            break;
          }
        }
        if(result > 0) break;
      }
    }

    private Dictionary<int, List<Location>> BuildNumLocation(Dictionary<int, int[,]> boards){
      Dictionary<int, List<Location>> result = new Dictionary<int, List<Location>>();
      foreach(var kvp in boards){
        var board = kvp.Value;
        for(int i = 0; i < 5; i++){
          for(int j = 0; j < 5; j++){
            if(!result.ContainsKey(board[i,j])) result[board[i,j]] = new List<Location>();
            result[board[i,j]].Add(new Location(kvp.Key, i, j));
          }
        }
      }
      return result;
    }

    private bool Check(int[,] board, Location loc){
      board[loc.Row, loc.Col] = -1;
      bool isRow = true;
      bool isCol = true;
      for(int i = 0; i < 5; i++){
        if(board[i, loc.Col] != -1) isCol = false;
        if(board[loc.Row, i] != -1) isRow = false;
      }
      return isRow || isCol;
    }

    private int CalculateAnswer(int[,] board, int num){
      int result = 0;
      for(int i = 0; i < 5; i++){
        for(int j = 0; j < 5; j++){
          if(board[i, j] != -1) result += board[i, j];
        }
      }
      result *= num;
      return result;
    }
  }

  public class Location{
    public int BoardId{get; set;}
    public int Row{get; set;}
    public int Col{get; set;}
    public Location(int id, int r, int c)
    {
      BoardId = id;
      Row = r;
      Col = c;
    }
  }
}
