using System.Text;
namespace AdventOfCode{
  public class TrenchMap : IPuzzle {

    int[,] steps = {{-1, -1}, {-1, 0}, {-1, 1}, {0, -1}, {0, 0}, {0, 1}, {1, -1}, {1, 0}, {1, 1}};
    public void Run(){
      InputReader inputReader = new InputReader();
      var input = inputReader.GetInputAsString("TrenchMap.txt");
      var algo = input[0];
      List<List<char>> image = new List<List<char>>();
      image.Add(MakeEmptyRow(input[2].Length + 4));
      image.Add(MakeEmptyRow(input[2].Length + 4));
      for(int i = 2; i < input.Count; i++){
        List<char> row = new List<char>(){'0', '0'};
        foreach(var c in input[i]){
          row.Add(c == '.' ? '0' : '1' );
        }
        row.Add('0');
        row.Add('0');
        image.Add(row);
      }
      image.Add(MakeEmptyRow(input[2].Length + 4));
      image.Add(MakeEmptyRow(input[2].Length + 4));
      //PrintImage(image);
      int run = 2;
      List<List<char>> newImage = CopyImage(image);
      while(run > 0){
        for(int i = run - 1; i < image.Count - (run - 1); i++){
          for(int j = run - 1; j < image[i].Count - (run - 1); j++){
            var code = GetImageCode(image, i, j);
            int idx = GetIndex(code);
            char c = algo[idx] == '#' ? '1' : '0';
            newImage[i][j] = c;
          }
        }
        image = CopyImage(newImage);
        run--;
        //PrintImage(image);
      }
      int result = 0;
      for(int i = 0; i < image.Count; i++){
        for(int j = 0; j < image[i].Count; j++){
          result += image[i][j] == '0' ? 0 : 1;
        }
      }
      Console.WriteLine("First: " + result);
    }

    private string GetImageCode(List<List<char>> image, int x, int y){
      StringBuilder sb = new StringBuilder();
      for(int i = 0; i < 9; i++){
        int r = x + steps[i, 0];
        int c = y + steps[i, 1];
        sb.Append(IsValid(image, r, c) ? image[r][c] : '0');
      }
      return sb.ToString();
    }
    private int GetIndex(string binaryString){
      return Convert.ToInt32(binaryString, 2);
    }

    private bool IsValid(List<List<char>> image, int r, int c){
      if(r < 0 || r >= image.Count) return false;
      if(c < 0 || c >= image[r].Count) return false;
      return true;
    }

    private List<char> MakeEmptyRow(int size){
      List<char> row = new List<char>();
      for(int i = 0; i < size; i++){
        row.Add('0');
      }
      return row;
    }

    private void PrintImage(List<List<char>> image){
      for(int i = 0; i < image.Count; i++){
        Console.WriteLine(new string(image[i].ToArray()));
      }
      Console.WriteLine("------------------------");
    }

    private List<List<char>> CopyImage(List<List<char>> image){
      List<List<char>> newImage = new List<List<char>>();
      for(int i = 0; i < image.Count; i++){
        List<char> row = new List<char>();
        for(int j = 0; j < image[i].Count; j++){
          row.Add(image[i][j]);
        }
        newImage.Add(row);
      }
      return newImage;
    }
  }
}
