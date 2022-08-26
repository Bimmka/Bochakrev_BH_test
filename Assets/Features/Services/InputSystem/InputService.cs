using UnityEngine;

namespace Features.Services.InputSystem
{
  public class InputService : IInputService
  {
    private const string HorizontalMove = "Horizontal";
    private const string VerticalMove = "Vertical";
    private const string HorizontalCamera = "Mouse X";
    private const string VerticalCamera = "Mouse Y";
    private const KeyCode SpecialActionKeyCode = KeyCode.F;
    
    private readonly InputCommandsContainer commandContainer;

    public InputService()
    {
      commandContainer = new InputCommandsContainer();
    }

    public void Cleanup() { }
    

    public void ReadInput(IInputCommand[] readedInputs, ref int inputIndex)
    {
      IInputCommand command;

      if (IsFitInLength(readedInputs, inputIndex))
      {
        command = commandContainer.Command(InputCommandType.Move);
        ((InputCommandVector)command).SetValue(MoveValue());

        AddCommand(readedInputs, ref inputIndex, command);
      }

      if (IsFitInLength(readedInputs, inputIndex))
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

    private bool IsFitInLength(IInputCommand[] readedInputs, int inputIndex)
    {
      return inputIndex < readedInputs.Length;
    }

    private void AddCommand(IInputCommand[] readedInputs, ref int index, IInputCommand command)
    {
      readedInputs[index] = command;
      index++;
    }

    private Vector2 MoveValue() => 
      new Vector2(Input.GetAxis(HorizontalMove), Input.GetAxis(VerticalMove));

    private Vector2 CameraRotateValue() => 
      new Vector2(Input.GetAxis(HorizontalCamera), Input.GetAxis(VerticalCamera));

    private bool IsSpecialActionPressed() => 
      Input.GetKeyDown(SpecialActionKeyCode);
  }
}