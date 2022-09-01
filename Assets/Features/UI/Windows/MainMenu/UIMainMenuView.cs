using TMPro;
using UnityEngine;

namespace Features.UI.Windows.MainMenu
{
  public class UIMainMenuView : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI joinLobbyIdErrorTip;
    [SerializeField] private TextMeshProUGUI hostLobbyIdErrorTip;

    public void HideTips()
    {
      ChangeTextEnableState(joinLobbyIdErrorTip, false);
      ChangeTextEnableState(hostLobbyIdErrorTip, false);
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