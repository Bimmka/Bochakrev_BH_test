using Features.Animatons;
using Features.Player.Scripts.Damage;
using Features.Player.Scripts.HeroCamera;
using Features.Player.Scripts.HeroInput;
using Features.Player.Scripts.HeroMachine.Base;
using Features.Player.Scripts.Move;
using Features.Player.Scripts.Rotate;
using Features.Services.InputSystem;
using Features.Services.LevelScore;
using Features.StaticData.HeroData.CameraRotate;
using Features.StaticData.HeroData.Dash;
using Features.StaticData.HeroData.Models;
using Features.StaticData.HeroData.Move;
using Features.StaticData.HeroData.Rotate;
using Mirror;
using UnityEngine;

namespace Features.Player.Scripts.Base
{
    [RequireComponent(typeof(HeroStateMachineObserver))]
    [RequireComponent(typeof(HeroInputObserver))]
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(HeroDamageHandler))]
    public class Hero : NetworkBehaviour
    {
        [SerializeField] private HeroInputObserver input;
        [SerializeField] private HeroStateMachineObserver stateMachineObserver;
        [SerializeField] private CharacterController characterController;
        [SerializeField] private HeroDamageHandler damageHandler;
        [SerializeField] private HeroMoveStaticData moveStaticData;
        [SerializeField] private HeroRotateStaticData rotateStaticData;
        [SerializeField] private HeroCameraStaticData cameraStaticData;
        [SerializeField] private HeroDashStaticData dashStaticData;

        private HeroCameraObserver cameraRotator;
        private ILevelScoreService levelScoreService;
        private SimpleAnimator animator;
        private HeroModel model;
        
        private string heroName;

        public void Construct(ILevelScoreService levelScoreService, IInputService inputService, string heroName, HeroModel model)
        {
            this.model = model;
            animator = model.Animator;
            this.heroName = heroName;
            this.levelScoreService = levelScoreService;
            input.Construct(inputService);
            damageHandler.Construct(model.MainRenderer);
            
            /* if (IsNotLocalPlayer())
               return;*/

            InitializeStateMachine();
        }

        private void OnDestroy()
        {
            /*if (IsNotLocalPlayer())
                return;*/
            
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
            Camera camera = Camera.main;
            
            HeroRotate rotate = new HeroRotate(transform, rotateStaticData);
            HeroMove move = new HeroMove(transform, moveStaticData, camera.transform, rotate, characterController);
            cameraRotator = new HeroCameraObserver(camera.transform, cameraStaticData, model.CameraTarget);
            cameraRotator.InitializeCamera();
            
            HeroStatesContainer container = new HeroStatesContainer(stateMachineObserver, move, cameraRotator, animator, dashStaticData, characterController, 
                levelScoreService, heroName);
            
            stateMachineObserver.Construct(container, animator);
        }

        private void Update()
        {
            /*if (IsNotLocalPlayer())
                return;*/
            
            input.ReadInput();
            stateMachineObserver.UpdateState(input.Commands, input.CommandsCount, Time.deltaTime);
            cameraRotator.Update(Time.deltaTime);
            input.ClearInput();
        }


        private bool IsNotLocalPlayer() => 
            isLocalPlayer == false;
    }
}
