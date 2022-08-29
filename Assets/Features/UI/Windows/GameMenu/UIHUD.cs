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

        private void DisplayScore(Dictionary<string, int> scores)
        {
            scoreDisplay.text = "";
            foreach (KeyValuePair<string,int> score in scores)
            {
                scoreDisplay.text += $"{score.Key} : {score.Value}\n";
            }
        }
    }
}
