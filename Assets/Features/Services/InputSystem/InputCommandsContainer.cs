using System;
using UnityEngine;

namespace Features.Services.InputSystem
{
  public class InputCommandsContainer
  {
    private readonly IInputCommand[] commands;

    public InputCommandsContainer()
    {
      string[] names = Enum.GetNames(typeof(InputCommandType));
      commands = new IInputCommand[names.Length];

      for (int i = 0; i < names.Length; i++)
      {
        commands[i] = CreateCommand((InputCommandType) Enum.Parse(typeof(InputCommandType), names[i]));
      }
    }

    public IInputCommand Command(InputCommandType type)
    {
      for (int i = 0; i < commands.Length; i++)
      {
        if (commands[i].Type == type)
          return commands[i];
      }

      return null;
    }

    private IInputCommand CreateCommand(InputCommandType type)
    {
      switch (type)
      {
        case InputCommandType.Move:
          return new InputCommandVector(InputCommandType.Move, Vector2.zero);
        case InputCommandType.SpecialAction:
          return new InputCommandBool(InputCommandType.SpecialAction, false);
        case InputCommandType.CameraRotate:
          return new InputCommandAxis(InputCommandType.CameraRotate, 0);
        default:
          throw new ArgumentOutOfRangeException(nameof(type), type, null);
      }
    }
  }
}