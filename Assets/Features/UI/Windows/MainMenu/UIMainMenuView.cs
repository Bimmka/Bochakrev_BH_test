using TMPro;
using UnityEngine;

namespace Features.UI.Windows.MainMenu
{
  public class UIMainMenuView : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI nicknameErrorTip;
    [SerializeField] private TextMeshProUGUI joinLobbyIdErrorTip;
    [SerializeField] private TextMeshProUGUI hostLobbyIdErrorTip;

    public void HideTips()
    {
      ChangeTextEnableState(nicknameErrorTip, false);
      ChangeTextEnableState(joinLobbyIdErrorTip, false);
      ChangeTextEnableState(hostLobbyIdErrorTip, false);
    }
    
    public void DisplayIncorrectNicknameTip()
    {
      ChangeTextEnableState(nicknameErrorTip, true);
    }

    public void DisplayIncorrectJoinLobbyIDTip()
    {
      ChangeTextEnableState(joinLobbyIdErrorTip, true);
    }

    public void DisplayIncorrectHostLobbyIDTip()
    {
      ChangeTextEnableState(hostLobbyIdErrorTip, true);
    }

    private void ChangeTextEnableState(TextMeshProUGUI text, bool isEnable)
    {
      if (text.enabled == isEnable)
        return;

      text.enabled = isEnable;
    }
  }
}