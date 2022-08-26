using Features.Animatons;
using Features.Player.Scripts.HeroCamera;
using Features.Player.Scripts.HeroMachine.Base;
using Features.Player.Scripts.Move;
using Features.Services.InputSystem;
using UnityEngine;

namespace Features.Player.Scripts.HeroMachine.States
{
  public class HeroMoveState : HeroStateMachineState
  {
    private readonly HeroMove move;
    private readonly CameraRotator cameraRotator;

    public HeroMoveState(HeroStateMachineObserver hero, HeroMove move, CameraRotator cameraRotator, SimpleAnimator animator, string parameterName) : 
      base(hero, animator, parameterName)
    {
      this.move = move;
      this.cameraRotator = cameraRotator;
    }

    public override void Update(IInputCommand[] commands, int commandsCount, float deltaTime)
    {
      base.Update(commands, commandsCount, deltaTime);
      
      if (commandsCount == 0)
        return;
      
      for (int i = 0; i < commandsCount; i++)
      {
        ApplyCommand(commands[i], deltaTime);
      }
    }
    
    protected override void ApplyMoveCommand(InputCommandVector command, float deltaTime)
    {
      base.ApplyMoveCommand(command, deltaTime);
      
      if (command.Vector == Vector2.zero)
        ChangeState<HeroIdleState>();
      else
        move.Move(command.Vector, deltaTime);
    }

    protected override void ApplyCameraRotateCommand(InputCommandVector command, float deltaTime)
    {
      base.ApplyCameraRotateCommand(command, deltaTime);
      
      cameraRotator.Rotate(command.Vector);
    }
  }
}