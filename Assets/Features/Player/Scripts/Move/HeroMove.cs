using Features.Extensions;
using Features.Player.Scripts.Rotate;
using Features.StaticData.Hero.Move;
using UnityEngine;

namespace Features.Player.Scripts.Move
{
  public class HeroMove
  {
    private readonly Transform heroTransform;
    private readonly HeroMoveStaticData moveData;
    private readonly Transform camera;
    private readonly HeroRotate rotate;
    private readonly CharacterController heroController;

    public HeroMove(Transform heroTransform, HeroMoveStaticData moveData, Transform camera, HeroRotate rotate, CharacterController heroController)
    {
      this.heroTransform = heroTransform;
      this.moveData = moveData;
      this.camera = camera;
      this.rotate = rotate;
      this.heroController = heroController;
    }

    public void Move(Vector2 direction, float deltaTime)
    {
      Vector3 moveDirection = InputMoveDirection(direction);
      if (heroTransform.forward.IsEqualMoveDirection(moveDirection) == false)
        rotate.WalkRotate(moveDirection);

      heroController.Move(moveDirection * (moveData.WalkSpeed * deltaTime));
    }
    
    public void Dash(Vector3 direction, float deltaTime, float step) => 
      heroController.Move(direction * (step * deltaTime));

    private Vector3 InputMoveDirection(Vector2 inputDirection)
    {
      Vector3 worldMoveVector = Vector3.zero;
      if (inputDirection.x != 0)
        worldMoveVector += camera.transform.right * inputDirection.x;

      if (inputDirection.y != 0)
        worldMoveVector += camera.transform.forward * inputDirection.y;

      worldMoveVector.y = 0;
      return worldMoveVector.normalized;
    }
  }
}