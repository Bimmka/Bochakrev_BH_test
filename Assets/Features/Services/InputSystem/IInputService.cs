namespace Features.Services.InputSystem
{
  public interface IInputService : ICleanupService
  {
    void ReadInput(IInputCommand[] readedInputs, ref int inputIndex);
  }
}