using UnityEngine;

namespace Features.Services.InputSystem
{
  public abstract class InputCommand<TValue> : IInputCommand
  {
    public InputCommandType Type { get; }

    public InputCommand(InputCommandType type, TValue value)
    {
      Type = type;
    }
    
    public abstract void SetValue(TValue newValue);
  }

  public class InputCommandVector : InputCommand<Vector2>, IInputCommandVector
  {
    public Vector2 Vector { get; private set; }

    public InputCommandVector(InputCommandType type, Vector2 value) : base(type, value)
    {
      SetValue(value);
    }

    public sealed override void SetValue(Vector2 newValue)
    {
      Vector = newValue;
    }
  }
  
  public class InputCommandAxis : InputCommand<float>, IInputCommandAxis
  {
    public float Axis { get; private set; }

    public InputCommandAxis(InputCommandType type, float value) : base(type, value)
    {
      SetValue(value);
    }

    public sealed override void SetValue(float newValue)
    {
      Axis = newValue;
    }
  }
  
  public class InputCommandBool : InputCommand<bool>, IInputCommandBool
  {
    public bool Bool { get; private set; }

    public InputCommandBool(InputCommandType type, bool value) : base(type, value)
    {
      SetValue(value);
    }

    public sealed override void SetValue(bool newValue)
    {
      Bool = newValue;
    }
  }
}