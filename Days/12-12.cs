using System.Text;
namespace AdventOfCode {
  public class PassagePathing : IPuzzle{
    long uniquePaths = 0;
    HashSet<string> uP = new HashSet<string>();
    public void Run(){
      InputReader inputReader = new InputReader();
      var input = inputReader.GetInputAsString("PassagePathing.txt");
      Dictionary<string, List<string>> paths = new Dictionary<string, List<string>>();
      foreach(var i in input){
        var p = i.Split('-');
        if(!paths.ContainsKey(p[0])) paths[p[0]] = new List<string>();
        if(p[1] != "start") paths[p[0]].Add(p[1]);
        if(!paths.ContainsKey(p[1])) paths[p[1]] = new List<string>();
        if(p[0] != "start") paths[p[1]].Add(p[0]);
      }
      foreach(var kvp in paths){
        kvp.Value.Sort();
      }
      foreach(var i in paths["start"]){
        DFS(paths, i, new HashSet<string>(){"start"}, false, new List<string>(){"start"});
      }
      Console.WriteLine("Second: " + uniquePaths);
    }

    private void DFS(Dictionary<string, List<string>> paths, string start, HashSet<string> visited, bool isUsed, List<string> path){
      path.Add(start);
      if(start == "end"){
        if(uP.Add(string.Join(",", path)))
          uniquePaths++;
        path.RemoveAt(path.Count - 1);
        return;
      }
      if(start.All(char.IsUpper)){
        foreach(var i in paths[start]){
          if(!visited.Contains(i)){
            DFS(paths, i, visited, isUsed, path);
          }
        }
      }else{
        if(!isUsed){
          foreach(var i in paths[start]){
            if(!visited.Contains(i)){
              DFS(paths, i, visited, true, path);
            }
          }
        }
        visited.Add(start);
        foreach(var i in paths[start]){
            if(!visited.Contains(i)){
              DFS(paths, i, visited, isUsed, path);
            }
          }
      }
      if(visited.Contains(start)) visited.Remove(start);
      path.RemoveAt(path.Count - 1);
    }
  }
}
