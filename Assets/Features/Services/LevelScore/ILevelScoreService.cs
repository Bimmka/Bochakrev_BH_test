using System;
using System.Collections.Generic;

namespace Features.Services.LevelScore
{
  public interface ILevelScoreService : IService
  {
    event Action<List<PlayerScore>> Changed;
    event Action<List<PlayerScore>> NetworkChanged; 
    void RegisterPlayer(string nickname);
    void RemovePlayer(string nickname);
    void ResetScore();
    void AddScore(string nickname, int count);
    void Change(List<PlayerScore> newScores);
  }
}