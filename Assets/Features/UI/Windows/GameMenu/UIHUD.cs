using System.Collections.Generic;
using Features.Services.LevelScore;
using Features.UI.Windows.Base;
using TMPro;
using UnityEngine;

namespace Features.UI.Windows.GameMenu
{
    public class UIHUD : BaseWindow
    {
        [SerializeField] private TextMeshProUGUI scoreDisplay;
        
        private ILevelScoreService levelScoreService;

        public void Construct(ILevelScoreService levelScoreService)
        {
            this.levelScoreService = levelScoreService;
            this.levelScoreService.Changed += DisplayScore;
        }

        protected override void Cleanup()
        {
            base.Cleanup();
            levelScoreService.Changed -= DisplayScore;
        }

        private void DisplayScore(List<PlayerScore> scores)
        {
            scoreDisplay.text = "";
            for (int i = 0; i < scores.Count; i++)
            {
                scoreDisplay.text += $"{scores[i].Nickname} : {scores[i].Score}\n";
            }
        }
    }
}
