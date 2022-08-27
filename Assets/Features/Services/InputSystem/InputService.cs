using Features.StaticData.InputBindings;
using UnityEngine;

namespace Features.Services.InputSystem
{
  public class InputService : IInputService
  {
    private readonly InputBindingsStaticData bindingsData;
    private readonly InputCommandsContainer commandContainer;

    public bool IsCleanedUp { get; private set; }

    public InputService(InputBindingsStaticData bindingsData)
    {
      this.bindingsData = bindingsData;
      commandContainer = new InputCommandsContainer();
    }

    public void Cleanup()
    {
      IsCleanedUp = true;
    }

    public void ReadInput(IInputCommand[] readedInputs, ref int inputIndex)
    {
      IInputCommand command;

      if (IsFitInLength(readedInputs, inputIndex))
      {
        command = commandContainer.Command(InputCommandType.Move);
        ((InputCommandVector)command).SetValue(MoveValue());

        AddCommand(readedInputs, ref inputIndex, command);
      }

      if (IsCameraRotatePressed() && IsFitInLength(readedInputs, inputIndex))
      {
        command = commandContainer.Command(InputCommandType.CameraRotate);
        
        ((InputCommandVector)command).SetValue(CameraRotateValue());

        AddCommand(readedInputs, ref inputIndex, command);
      }

      if (IsSpecialActionPressed() && IsFitInLength(readedInputs, inputIndex))
      {
        command = commandContainer.Command(InputCommandType.SpecialAction);
        
        ((InputCommandBool)command).SetValue(true);

        AddCommand(readedInputs, ref inputIndex, command);
      }
      
    }

    private bool IsFitInLength(IInputCommand[] readedInputs, int inputIndex) => 
      inputIndex < readedInputs.Length;

    private void AddCommand(IInputCommand[] readedInputs, ref int index, IInputCommand command)
    {
      readedInputs[index] = command;
      index++;
    }

    private Vector2 MoveValue() => 
      new Vector2(Input.GetAxis(bindingsData.HorizontalMove), Input.GetAxis(bindingsData.VerticalMove));

    private Vector2 CameraRotateValue() => 
      new Vector2(Input.GetAxis(bindingsData.VerticalCameraMove), Input.GetAxis(bindingsData.HorizontalCameraMove));

    private bool IsSpecialActionPressed() => 
      Input.GetKeyDown(bindingsData.SpecialActionKeyCode);

    private bool IsCameraRotatePressed() => 
      Input.GetKey(bindingsData.CameraRotateButton);
  }
}