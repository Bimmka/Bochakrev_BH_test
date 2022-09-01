using System.Collections.Generic;
using Mirror;

namespace Features.Services.LevelScore
{
  public class ScoreNetwork : NetworkBehaviour
  {
    private readonly SyncList<PlayerScore> Scores = new SyncList<PlayerScore>();
    public List<PlayerScore> SavedPlayerScore;
    
    private ILevelScoreService levelScoreService;

    public void Construct(ILevelScoreService levelScoreService)
    {
      this.levelScoreService = levelScoreService;
      this.levelScoreService.NetworkChanged += CmdChanged;
    }

    private void OnDestroy()
    {
      levelScoreService.NetworkChanged -= CmdChanged;
    }

    public override void OnStartClient()
    {
      base.OnStartClient();

      Scores.Callback += SyncVector3Vars;
      SavedPlayerScore = new List<PlayerScore>(Scores.Count);
      for (int i = 0; i < Scores.Count; i++)
      {
        SavedPlayerScore.Add(Scores[i]);
      }

      levelScoreService.Change(SavedPlayerScore);
    }

    [Server]
    private void ChangeScore(List<PlayerScore> newScore)
    {
      for (int i = 0; i < Scores.Count && i < newScore.Count; i++)
      {
        Scores[i] = newScore[i];
      }
      
      if (Scores.Count < newScore.Count)
        for (int i = Scores.Count; i < newScore.Count; i++)
        {
          Scores.Add(newScore[i]);
        }
      else if (Scores.Count > newScore.Count)
        while (Scores.Count > newScore.Count)
        {
          Scores.Remove(Scores[Scores.Count - 1]);
        }
    }

    [Command]
    public void CmdChangeScore(List<PlayerScore> newScore)
    {
      ChangeScore(newScore);
    }

    private void SyncVector3Vars(SyncList<PlayerScore>.Operation op, int index, PlayerScore oldItem,
      PlayerScore newItem)
    {
      switch (op)
      {
        case SyncList<PlayerScore>.Operation.OP_ADD:
        {
          SavedPlayerScore.Add(newItem);
          break;
        }
        case SyncList<PlayerScore>.Operation.OP_CLEAR:
        {
          SavedPlayerScore.Clear();
          break;
        }
        case SyncList<PlayerScore>.Operation.OP_INSERT:
        {

          break;
        }
        case SyncList<PlayerScore>.Operation.OP_REMOVEAT:
        {

          break;
        }
        case SyncList<PlayerScore>.Operation.OP_SET:
        {
          SavedPlayerScore[index] = newItem;
          break;
        }
      }
      
      levelScoreService.Change(SavedPlayerScore);
    }

    private void CmdChanged(List<PlayerScore> scores)
    {
      if (isServer)
        ChangeScore(scores);
      else
        CmdChangeScore(scores);
    }
  }
}