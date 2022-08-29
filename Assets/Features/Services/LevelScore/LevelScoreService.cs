using System;
using System.Collections.Generic;

namespace Features.Services.LevelScore
{
    public class LevelScoreService : ILevelScoreService
    {
        private Dictionary<string, int> score;

        public event Action<Dictionary<string, int>> Changed;

        public LevelScoreService()
        {
            score = new Dictionary<string, int>(5);
        }

        public void RegisterPlayer(string nickname)
        {
            if (IsContains(nickname))
                return;
        
            score.Add(nickname, 0);
            NotifyAboutChangeScore();
        
        }

        public void RemovePlayer(string nickname)
        {
            if (IsContains(nickname) == false)
                return;

            score.Remove(nickname);
            NotifyAboutChangeScore();
        }

        public void AddScore(string nickname, int count)
        {
            if (IsContains(nickname) == false)
                return;

            score[nickname] += count;
            NotifyAboutChangeScore();
        }

        public void ResetScore()
        {
            foreach (string key in score.Keys)
            {
                score[key] = 0;
            }
        
            Changed?.Invoke(score);
        }

        private void NotifyAboutChangeScore() => 
            Changed?.Invoke(score);

        private bool IsContains(string nickname) => 
            score.ContainsKey(nickname);
    }
}
