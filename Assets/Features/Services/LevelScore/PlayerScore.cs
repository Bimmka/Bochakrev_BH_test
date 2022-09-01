namespace Features.Services.LevelScore
{
  public class PlayerScore
  {
    public string Nickname;
    public int Score;

    public PlayerScore()
    {
      
    }

    public PlayerScore(string nickname, int score)
    {
      Nickname = nickname;
      Score = score;
    }

    public void IncScore(int count) => 
      Score+=count;

    public void ResetScore() => 
      Score = 0;
  }
}