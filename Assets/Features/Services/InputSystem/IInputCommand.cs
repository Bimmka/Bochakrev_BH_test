using UnityEngine;

namespace Features.Services.InputSystem
{
  public interface IInputCommand
  {
    InputCommandType Type { get; }
  }

  public interface IInputCommandVector : IInputCommand
  {
    Vector2 Vector { get; }
  }
  
  public interface IInputCommandAxis : IInputCommand
  {
    float Axis { get; }
  }
  
  public interface IInputCommandBool : IInputCommand
  {
    bool Bool { get; }
  }
}