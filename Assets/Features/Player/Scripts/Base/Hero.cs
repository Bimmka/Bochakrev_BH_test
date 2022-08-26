using System;
using Features.Animatons;
using Features.Player.Scripts.HeroCamera;
using Features.Player.Scripts.HeroInput;
using Features.Player.Scripts.HeroMachine.Base;
using Features.Player.Scripts.Move;
using Features.Player.Scripts.Rotate;
using Features.StaticData.Hero;
using Features.StaticData.Hero.CameraRotate;
using Features.StaticData.Hero.Move;
using Features.StaticData.Hero.Rotate;
using Mirror;
using UnityEngine;

namespace Features.Player.Scripts.Base
{
    public class Hero : NetworkBehaviour
    {
        [SerializeField] private HeroInputObserver input;
        [SerializeField] private HeroStateMachineObserver stateMachineObserver;
        [SerializeField] private CharacterController characterController;
        [SerializeField] private HeroMoveStaticData moveStaticData;
        [SerializeField] private HeroRotateStaticData rotateStaticData;
        [SerializeField] private HeroCameraRotateStaticData cameraRotateStaticData;
        [SerializeField] private SimpleAnimator animator;

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
            ConstructStateMachine();
            stateMachineObserver.Subscribe();
            stateMachineObserver.CreateStates();
            stateMachineObserver.SetDefaultState();
        }

        private void ConstructStateMachine()
        {
            Transform cameraTransform = Camera.main.transform;
            
            HeroRotate rotate = new HeroRotate(transform, rotateStaticData);
            HeroMove move = new HeroMove(transform, moveStaticData, cameraTransform, rotate, characterController);
            CameraRotator cameraRotator = new CameraRotator(cameraTransform, cameraRotateStaticData);
            
            HeroStatesContainer container = new HeroStatesContainer(stateMachineObserver, move, cameraRotator, animator);
            
            stateMachineObserver.Construct(container);
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
