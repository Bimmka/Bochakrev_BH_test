using Features.Services.InputSystem;
using UnityEngine;

namespace Features.Player.Scripts.HeroInput
{
  public class HeroInputObserver : MonoBehaviour 
  {
    private readonly IInputCommand[] readedInputs = new IInputCommand[20];
    
    private int inputIndex = 0;

    private IInputService inputService;

    public IInputCommand[] Commands => readedInputs;
    public int CommandsCount => inputIndex;

    
    public void Construct(IInputService inputService)
    {
      this.inputService = inputService;
    }

    public void Cleanup()
    {
      inputService.Cleanup();
    }
    public void LockInput() { }
    public void UnlockInput() { }

    public void ReadInput() => 
      inputService.ReadInput(readedInputs, ref inputIndex);

    public void ClearInput() => 
      inputIndex = 0;
  }
}