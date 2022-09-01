using System;
using Features.Constants;
using Features.GameStates;
using Features.GameStates.States;
using Features.Services.Network;
using Features.UI.Windows.Base;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Features.UI.Windows.MainMenu
{
  [RequireComponent(typeof(UIMainMenuView))]
  public class UIMainMenu : BaseWindow
  {
    [SerializeField] private UIMainMenuView view;
    [SerializeField] private TMP_InputField joinLobbyIdInputField;
    [SerializeField] private TMP_InputField hostLobbyIdInputField;
    [SerializeField] private Button joinLobbyButton;
    [SerializeField] private Button hostLobbyButton;
    
    private INetwork network;
    private IGameStateMachine gameStateMachine;

    public void Construct(INetwork network, IGameStateMachine gameStateMachine)
    {
      this.gameStateMachine = gameStateMachine;
      this.network = network;
    }

    protected override void Initialize()
    {
      base.Initialize();
      joinLobbyIdInputField.text = GameConstants.DefaultLobbyID;
      hostLobbyIdInputField.text = GameConstants.DefaultLobbyID;
    }

    protected override void Subscribe()
    {
      base.Subscribe();
      joinLobbyButton.onClick.AddListener(TryJoinLobby);
      hostLobbyButton.onClick.AddListener(TryHostLobby);
    }

    protected override void Cleanup()
    {
      base.Cleanup();
      joinLobbyButton.onClick.RemoveListener(TryJoinLobby);
      hostLobbyButton.onClick.RemoveListener(TryHostLobby);
    }

    private void TryJoinLobby()
    {
      view.HideTips();

      if (IsCorrectJoinLobbyID() == false)
      {
        view.DisplayIncorrectJoinLobbyIDTip();
        return;
      }

      JoinLobby();
    }

    private void JoinLobby()
    {
      GameLoadState state = gameStateMachine.GetState<GameLoadState>();
      gameStateMachine.Enter<GameLoadState>();
      state.LoadAsClient(joinLobbyIdInputField.text);
    }

    private void TryHostLobby()
    {
      view.HideTips();

      if (IsCorrectHostLobbyID() == false)
      {
        view.DisplayIncorrectHostLobbyIDTip();
        return;
      }
      
      HostLobby();
    }

    private void HostLobby()
    {
      GameLoadState state = gameStateMachine.GetState<GameLoadState>();
      gameStateMachine.Enter<GameLoadState>();
      state.LoadAsHost();
    }

    private bool IsCorrectJoinLobbyID() => 
      string.IsNullOrEmpty(joinLobbyIdInputField.text) == false;

    private bool IsCorrectHostLobbyID() => 
      string.IsNullOrEmpty(hostLobbyIdInputField.text) == false;
  }
}