using System;
using System.Collections.Generic;

namespace Features.Services.LevelScore
{
    public class LevelScoreService : ILevelScoreService
    {
        private List<PlayerScore> scores = new List<PlayerScore>(5);

        public event Action<List<PlayerScore>> Changed;
        public event Action<List<PlayerScore>> NetworkChanged;

        public void RegisterPlayer(string nickname)
        { 
           scores.Add(new PlayerScore(nickname, 0));
           NotifyAboutNetworkChangeScore();
           NotifyAboutChangeScore();
        }

        public void RemovePlayer(string nickname)
        {
            int index = PlayerIndex(nickname);

            if (index == -1)
                return;
            
            scores.Remove(scores[index]);
            NotifyAboutNetworkChangeScore();
            NotifyAboutChangeScore();
        }

        public void AddScore(string nickname, int count)
        {
            int index = PlayerIndex(nickname);

            if (index == -1)
                return;

            scores[index].IncScore(count);
            NotifyAboutNetworkChangeScore();
            NotifyAboutChangeScore();
        }

        public void Change(List<PlayerScore> newScores)
        {
            scores = new List<PlayerScore>(newScores);
            NotifyAboutChangeScore();
        }

        public void ResetScore()
        {
            for (int i = 0; i < scores.Count; i++)
            {
                scores[i].ResetScore();
            }

            NotifyAboutNetworkChangeScore();
            NotifyAboutChangeScore();
        }

        private int PlayerIndex(string nickname)
        {
            for (int i = 0; i < scores.Count; i++)
            {
                if (scores[i].Nickname == nickname)
                    return i;
            }

            return -1;
        }

        private void NotifyAboutChangeScore() => 
            Changed?.Invoke(scores);
        
        private void NotifyAboutNetworkChangeScore() => 
            NetworkChanged?.Invoke(scores);
    }
}
