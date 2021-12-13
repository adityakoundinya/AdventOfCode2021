using System.Text;
namespace AdventOfCode{
  public class TransparentOrigami:IPuzzle {
    public void Run(){
      InputReader inputReader = new InputReader();
      var input = inputReader.GetInputAsString("TransparentOrigami.txt");
      int i = 0;
      int maxX = 0;
      int maxY = 0;
      List<(int x, int y)> points = new List<(int x, int y)>();
      List<(string, int)> folds = new List<(string, int)>();
      for(;i < input.Count; i++){
        if(input[i] == string.Empty) break;
        var temp = input[i].Split(',');
        int x = int.Parse(temp[0]);
        int y = int.Parse(temp[1]);
        maxX = Math.Max(maxX, x);
        maxY = Math.Max(maxY, y);
        points.Add((x, y));
      }
      i++;
      while(i < input.Count){
        var temp = input[i].Split(' ')[2].Split('=');
        folds.Add((temp[0], int.Parse(temp[1])));
        i++;
      }
      int[,] grid = new int[maxX + 1, maxY + 1];
      foreach(var p in points){
        grid[p.x,p.y] = 1;
      }
      foreach(var f in folds){
        grid = f.Item1 == "x" ? FoldVertical(grid, f.Item2) : FoldHorizontal(grid, f.Item2);
      }
      int result = 0;
      for(int row = 0; row < grid.GetLength(0); row++){
        for(int col = 0; col < grid.GetLength(1); col++){
          if(grid[row,col] == 1) result++;
        }
      }
      Console.WriteLine("First: " + result);
      Console.WriteLine("Second:");
      PrintGrid(grid);
    }

    private int[,] FoldHorizontal(int[,] grid, int line){
      int s = 1;
      int[,] folded = new int[grid.GetLength(0), line];
      while(line + s < grid.GetLength(1)){
        for(int i = 0; i < grid.GetLength(0); i++){
          folded[i, line - s] = grid[i, line - s];
          if(grid[i, line - s] == 1) continue;
          folded[i, line - s] = grid[i, line + s];
        }
        s++;
      }
      return folded;
    }
    private int[,] FoldVertical(int[,] grid, int line){
      int s = 1;
      int[,] folded = new int[line, grid.GetLength(1)];
      while(line + s < grid.GetLength(0)){
        for(int i = 0; i < grid.GetLength(1); i++){
          folded[line - s, i] = grid[line - s, i];
          if(grid[line - s, i] == 1) continue;
          folded[line - s, i] = grid[line + s, i];
        }
        s++;
      }
      return folded;
    }

    private void PrintGrid(int[,] grid){
      for(int row = 0; row < grid.GetLength(1); row++){
        StringBuilder sb = new StringBuilder();
        for(int col = 0; col < grid.GetLength(0); col++){
          sb.Append(grid[col, row] == 1 ? "#" : ".");
        }
        Console.WriteLine(sb.ToString());
      }
    }
  }
}

