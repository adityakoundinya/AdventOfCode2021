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
      return input;
    }

    public List<int> GetInputAsInt(string fileName){
      var inputStrings = GetInputAsString(fileName);
      List<int> result = new List<int>();
      foreach(var i in inputStrings){
        result.Add(Convert.ToInt32(i));
      }
      return result;
    }

  }
}
