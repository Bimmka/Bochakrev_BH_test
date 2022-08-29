using Features.Services.UI.Factory;

namespace Features.Services.UI.Windows
{
  public interface IWindowsService : ICleanupService
  {
    void Open(WindowId windowId);
    void Close(WindowId windowId);
  }
}