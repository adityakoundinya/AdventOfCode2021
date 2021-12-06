namespace AdventOfCode{
  public class Lanternfish : IPuzzle{
    public void Run(){
      InputReader inputReader = new InputReader();
      var input = inputReader.GetInputAsIntFromCsv("Lanternfish.txt");
      List<Lineage> parents = new List<Lineage>();
      foreach(var i in input) parents.Add(new Lineage(i));
      long result = parents.Count;
      for(int i = 0; i < 80; i++){
        foreach(var c in parents){
          result += DFS(c);
        }
      }
      Console.WriteLine("First: " + result);
    }

    private long DFS(Lineage child){
      if(child == null) return 0;
      long result = 0;
      foreach(var c in child.Children){
        result += DFS(c);
      }
      child.Timer--;
      if(child.Timer == -1){
        child.Children.Add(new Lineage(8));
        child.Timer = 6;
        result++;
      }
      return result;
    }
  }

  public class Lineage{
    public int Timer { get; set; }
    public List<Lineage> Children { get; set; }
    public Lineage (int timer)
    {
      Timer = timer;
      Children = new List<Lineage>();
    }
  }
}
