using System.Text;
namespace AdventOfCode{
  public class InputReader{
    private const string _baseFolder = "Inputs/";
    public List<string> GetInputAsString(string fileName){
      List<string> input = new List<string>();
      FileStream fileStream = new FileStream(_baseFolder + fileName, FileMode.Open);
      using (StreamReader streamReader = new StreamReader(fileStream)){
        string? line = streamReader.ReadLine();
        while(line != null){
          input.Add(line);
          line = streamReader.ReadLine();
        }
      }
      fileStream.Close();
      fileStream.Dispose();
      return input;
    }

    public string GetInputAsBlob(string fileName){
      StringBuilder sb = new StringBuilder();
      FileStream fileStream = new FileStream(_baseFolder + fileName, FileMode.Open);
      using (StreamReader streamReader = new StreamReader(fileStream)){
        string? line = streamReader.ReadLine();
        while(line != null){
          sb.AppendLine(line);
          line = streamReader.ReadLine();
        }
      }
      fileStream.Close();
      fileStream.Dispose();
      return sb.ToString();
    }

    public List<int> GetInputAsInt(string fileName){
      var inputStrings = GetInputAsString(fileName);
      List<int> result = new List<int>();
      foreach(var i in inputStrings){
        result.Add(Convert.ToInt32(i));
      }
      return result;
    }

    public List<int> GetDrawingInputForGiantSquid(string fileName){
      List<int> result = new List<int>();
      FileStream fileStream = new FileStream(_baseFolder + fileName, FileMode.Open);
      using (StreamReader streamReader = new StreamReader(fileStream)){
        string? line = streamReader.ReadLine();
        if(line == null) return new List<int>();
        var num = line.Split(',');
        foreach(var i in num) result.Add(int.Parse(i));
      }
      fileStream.Close();
      fileStream.Dispose();
      return result;
    }

    public Dictionary<int, int[,]> GetBoardInputForGiantSquid(string fileName){
      Dictionary<int, int[,]> result = new Dictionary<int, int[,]>();
      FileStream fileStream = new FileStream(_baseFolder + fileName, FileMode.Open);
      using (StreamReader streamReader = new StreamReader(fileStream)){
        streamReader.ReadLine();
        string? line = streamReader.ReadLine();
        int id = 0;
        while(line != null){
          int[,] board = new int[5,5];
          for(int i = 0; i < 5; i++){
            line = streamReader.ReadLine();
            if(line == null) break;
            var num = line.Split(' ').ToList();
            num.RemoveAll(o=> o == string.Empty);
            for(int j = 0; j < 5; j++){
              board[i, j] = int.Parse(num[j]);
            }
          }
          result[id++] = board;
          line = streamReader.ReadLine();
        }
      }
      fileStream.Close();
      fileStream.Dispose();
      return result;
    }

    public List<int> GetInputAsIntFromCsv(string fileName){
      List<int> result = new List<int>();
      FileStream fileStream = new FileStream(_baseFolder + fileName, FileMode.Open);
      using (StreamReader streamReader = new StreamReader(fileStream)){
        string? line = streamReader.ReadLine();
        if(line != null){
          var num = line.Split(',');
          foreach(var i in num){
          result.Add(int.Parse(i));
          }
        }
      }
      fileStream.Close();
      fileStream.Dispose();
      return result;
    }
  }
}
