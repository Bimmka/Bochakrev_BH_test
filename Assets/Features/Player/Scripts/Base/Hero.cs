using System;
using Features.Player.Scripts.HeroInput;
using Features.Player.Scripts.HeroMachine.Base;
using Mirror;
using UnityEngine;

namespace Features.Player.Scripts.Base
{
    public class Hero : NetworkBehaviour
    {
        [SerializeField] private HeroInputObserver input;
        [SerializeField] private HeroStateMachineObserver stateMachineObserver;
        [SerializeField] private CharacterController characterController;

        private void Awake()
        {
            if (IsNotLocalPlayer())
                return;

            InitializeStateMachine();
        }

        private void OnDestroy()
        {
            if (IsNotLocalPlayer())
                return;
            
            input.Cleanup();
            stateMachineObserver.Cleanup();
        }

        private void InitializeStateMachine()
        {
            stateMachineObserver.Construct();
            stateMachineObserver.Subscribe();
            stateMachineObserver.CreateStates();
            stateMachineObserver.SetDefaultState();
        }

        private void Update()
        {
            if (IsNotLocalPlayer())
                return;
            
            input.ReadInput();
            stateMachineObserver.UpdateState(input.Commands, input.CommandsCount, Time.deltaTime);
            input.ClearInput();
        }


        private bool IsNotLocalPlayer() => 
            isLocalPlayer == false;
    }
}
