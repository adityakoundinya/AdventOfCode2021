namespace AdventOfCode {
  public class Chiton:IPuzzle{
    int[,] steps = {{-1, 0}, {0, -1}, {0, 1}, {1, 0}};
    public void Run(){
      InputReader inputReader = new InputReader();
      var input = inputReader.GetInputAsString("Chiton.txt");
      List<List<int>> matrix = new List<List<int>>();
      foreach(var i in input){
        List<int> row = new List<int>();
        foreach(var j in i) row.Add(int.Parse(j.ToString()));
        matrix.Add(row);
      }
      BFS(matrix, "First: ");
      var fullMap = GetFullMap(matrix);
      BFS(fullMap, "Second: ");
    }

    private void BFS(List<List<int>> matrix, string message){
      var memo = new HashSet<(int x, int y)>(){(0, 0)};
      SortedList<int, List<(int x, int y)>> q = new SortedList<int, List<(int x, int y)>>();
      q[0] = new List<(int x, int y)>(){(0, 0)};
      while(q.Count > 0){
        bool isBreak = false;
        var top = q.First();
        q.RemoveAt(0);
        foreach(var p in top.Value){
          if(p.x == matrix.Count - 1 && p.y == matrix[p.x].Count - 1){
            Console.WriteLine(message + top.Key);
            isBreak = true;
            break;
          }
          for(int i = 0; i < 4; i++){
            int r = p.x + steps[i, 0];
            int c = p.y + steps[i, 1];
            if(!isValid(matrix, r, c)) continue;
            if(!memo.Add((r, c))) continue;
            int cost = top.Key + matrix[r][c];
            if(!q.ContainsKey(cost))q[cost] = new List<(int x, int y)>();
            q[cost].Add((r, c));
          }
        }
        if(isBreak) break;
      }
    }
    private bool isValid(List<List<int>> matrix, int row, int col){
      if(row < 0 || row >= matrix.Count) return false;
      if(col < 0 || col >= matrix[row].Count) return false;
      return true;
    }
    private List<List<int>> GetFullMap(List<List<int>> matrix){
      int x = matrix.Count * 5;
      int y = matrix[0].Count * 5;
      List<List<int>> fullMap = new List<List<int>>();
      for(int i = 0; i < matrix.Count; i++){
        List<int> row = new List<int>();
        for(int j = 0; j < matrix[i].Count; j++){
          row.Add(matrix[i][j]);
        }
        fullMap.Add(row);
      }
      for(int i = 0; i < matrix.Count; i++){
        for(int j = matrix.Count; j < x; j++){
          int num = fullMap[i][j - matrix[i].Count] + 1;
          fullMap[i].Add(num > 9 ? 1 : num);
        }
      }
      for(int i = matrix.Count; i < x; i++){
        fullMap.Add(new List<int>());
        for(int j = 0; j < y; j++){
          int num = fullMap[i - matrix.Count][j] + 1;
          fullMap[i].Add(num > 9 ? 1 : num);
        }
      }
      return fullMap;
    }
  }
}
