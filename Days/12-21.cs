namespace AdventOfCode{
  public class DiracDice : IPuzzle {
    int totalDiceRolls = 0;
    int currentDiceRoll = 0;
    int[] dice = new int[100];

    long player1_wins;
    long player2_wins;

    Dictionary<int, int> oddsForNext3throws = new Dictionary<int, int>();
    public void Run(){
      int player1 = 3;
      int player2 = 5;
      int p1S = 0;
      int p2S = 0;
      for(int i = 1; i < 100; i++) dice[i - 1] = i;
      while(p1S < 1000 && p2S < 1000){
        int score = RollDice();
        score %= 10;
        player1 += score;
        player1 = player1 > 10 ? player1 % 10 : player1;
        p1S += player1;
        if(p1S >= 1000) break;
        score = RollDice();
        score %= 10;
        player2 += score;
        player2 = player2 > 10 ? player2 % 10 : player2;
        p2S += player2;
      }
      int loser = p1S >= 1000 ? p2S : p1S;
      int result = loser * totalDiceRolls;
      Console.WriteLine("First: " + result);

      player1 = 3;
      player2 = 5;

      player1_wins = 0;
      player2_wins = 0;

      oddsForNext3throws.Add(3, 1);
      oddsForNext3throws.Add(4, 3);
      oddsForNext3throws.Add(5, 6);
      oddsForNext3throws.Add(6, 7);
      oddsForNext3throws.Add(7, 6);
      oddsForNext3throws.Add(8, 3);
      oddsForNext3throws.Add(9, 1);

      //makeRound(player1setup, player2setup, true, 1, 1);
      makeRound2(player1, player2, 0, 0, 1, true);

      long second = player1_wins > player2_wins ? player1_wins : player2_wins;
      Console.WriteLine("Second: " + second);
    }

    public void makeRound2(int player1position, int player2position, int player1score, int player2score, long occurrences, bool player1) {
        foreach (int nextSteps in oddsForNext3throws.Keys)
        {
            if (player1)
            {
                int landOnTile = (player1position + nextSteps) % 10;
                if (landOnTile == 0)
                    landOnTile = 10;
                int score = player1score + landOnTile;
                long probability = occurrences * oddsForNext3throws[nextSteps];
                // If the player wins, add those numbers.
                if (score >= 21)
                {
                    player1_wins += probability;
                }
                else
                {
                    makeRound2(landOnTile, player2position, score, player2score, probability, false);
                }
            }
            else
            {
                int landOnTile = (player2position + nextSteps) % 10;
                if (landOnTile == 0)
                    landOnTile = 10;
                int score = player2score + landOnTile;
                long probability = occurrences * oddsForNext3throws[nextSteps];
                // If the player wins, add those numbers.
                if (score >= 21)
                {
                    player2_wins += probability;
                }
                else
                {
                    makeRound2(player1position, landOnTile, player1score, score, probability, true);
                }
            }
        }
      }
    private int RollDice(){
      int roll = 0;
      for(int i = 0; i < 3; i++){
        roll += dice[currentDiceRoll];
        currentDiceRoll++;
        currentDiceRoll %= 100;
      }
      totalDiceRolls += 3;
      return roll;
    }
  }
}
