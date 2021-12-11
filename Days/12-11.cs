namespace AdventOfCode{
  public class DumboOctopus : IPuzzle {
    int[,] steps = {{-1, -1}, {-1, 0}, {-1, 1}, {0, -1}, {0, 1}, {1, -1}, {1, 0}, {1, 1}};
    public void Run(){
      InputReader inputReader = new InputReader();
      var input = inputReader.GetInputAsString("DumboOctopus.txt");
      List<List<int>> matrix = new List<List<int>>();
      foreach(var i in input){
        List<int> row = new List<int>();
        foreach(var j in i) row.Add(int.Parse(j.ToString()));
        matrix.Add(row);
      }
      int flashes = 0;
      for(int k = 0;k < 100; k++){
        flashes += SimulateStep(matrix);
      }
      Console.WriteLine("First: " + flashes);

      matrix = new List<List<int>>();
      foreach(var i in input){
        List<int> row = new List<int>();
        foreach(var j in i) row.Add(int.Parse(j.ToString()));
        matrix.Add(row);
      }
      int q = 0;
      while(!isAllZero(matrix)){
        SimulateStep(matrix);
        q++;
      }
      Console.WriteLine("Second: " + q);
    }
    private bool isValid(List<List<int>> matrix, int row, int col){
      if(row < 0 || row >= matrix.Count) return false;
      if(col < 0 || col >= matrix[row].Count) return false;
      return true;
    }

    private bool isAllZero(List<List<int>> matrix){
      for(int i = 0; i < matrix.Count; i++){
        for(int j = 0; j < matrix[i].Count; j++){
          if(matrix[i][j] != 0){
            return false;
          }
        }
      }
      return true;
    }

    private int SimulateStep(List<List<int>> matrix){
      int flashes = 0;
      HashSet<(int r, int c)> set = new HashSet<(int, int)>();
        Stack<(int r, int c)> flash = new Stack<(int, int)>();
        for(int i = 0; i < matrix.Count; i++){
          for(int j = 0; j < matrix[i].Count; j++){
            matrix[i][j]++;
            if(matrix[i][j] > 9) {
              flash.Push((i, j));
              set.Add((i, j));
              flashes++;
            }
          }
        }
        while(flash.Count > 0){
          var point = flash.Pop();
          matrix[point.r][point.c] = 0;
          for(int s = 0; s < 8; s++){
            int r = point.r + steps[s, 0];
            int c = point.c + steps[s, 1];
            if(!isValid(matrix, r, c)) continue;
            if(set.Contains((r, c))) continue;
            matrix[r][c]++;
            if(matrix[r][c] > 9){
              flash.Push((r, c));
              set.Add((r, c));
              flashes++;
            }
          }
        }
      return flashes;
    }
  }
}
