using System;
using System.Collections.Generic;
using Features.Services;

public interface ILevelScoreService : IService
{
  event Action<Dictionary<string, int>> Changed;
  void RegisterPlayer(string nickname);
  void RemovePlayer(string nickname);
  void ResetScore();
  void AddScore(string nickname, int count);
}