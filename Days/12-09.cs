namespace AdventOfCode{
  public class SmokeBasin:IPuzzle{
    int[,] steps = {{-1, 0}, {0, 1}, {1, 0}, {0, -1}};
    public void Run(){
      InputReader inputReader = new InputReader();
      var input = inputReader.GetInputAsString("SmokeBasin.txt");
      List<List<int>> matrix = new List<List<int>>();
      foreach(var i in input){
        List<int> row = new List<int>();
        foreach(var j in i) row.Add(int.Parse(j.ToString()));
        matrix.Add(row);
      }
      int result = 0;
      Dictionary<(int, int), int> lows = new Dictionary<(int, int), int>();
      for(int i = 0; i < matrix.Count; i++){
        for(int j = 0; j < matrix[i].Count; j++){
          bool isAdd = true;
          for(int k = 0; k < 4; k++){
            int r = i + steps[k,0];
            int c = j + steps[k, 1];
            if(!isValid(matrix, r, c)) continue;
            if(matrix[i][j] == 9 || matrix[i][j] >= matrix[r][c]){
              isAdd = false;
              break;
            }
          }
          if(isAdd) lows.Add((i, j), matrix[i][j] + 1);
        }
      }
      foreach(var l in lows) result += l.Value;
      Console.WriteLine("First: " + result);

      result = 0;
      List<int> basins = new List<int>();
      foreach(var kvp in lows){
        var set = new HashSet<(int, int)>();
        set.Add((kvp.Key.Item1, kvp.Key.Item2));
        basins.Add(DFS(matrix, kvp.Key.Item1, kvp.Key.Item2, set));
      }
      var sorted = basins.OrderByDescending(o => o).ToList();
      result = sorted[0] * sorted[1] * sorted[2];
      Console.WriteLine("Second: " + result);
    }

    private int DFS(List<List<int>> matrix, int row, int col, HashSet<(int, int)> memo){
      int result = 0;
      int workingOn = matrix[row][col];
      for(int i = 0; i < 4; i++){
        int r = row + steps[i, 0];
        int c = col + steps[i, 1];
        if(!isValid(matrix, r, c) || memo.Contains((r, c))) continue;
        if(matrix[r][c] == 9) continue;
        if(matrix[row][col] >= matrix[r][c]) continue;
        int next = matrix[r][c];
        memo.Add((r, c));
        result += DFS(matrix, r, c, memo);
      }
      return result + 1;
    }

    private bool isValid(List<List<int>> matrix, int row, int col){
      if(row < 0 || row >= matrix.Count) return false;
      if(col < 0 || col >= matrix[row].Count) return false;
      return true;
    }
  }
}
