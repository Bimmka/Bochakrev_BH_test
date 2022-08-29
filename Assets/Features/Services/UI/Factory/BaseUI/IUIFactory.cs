using System;
using Features.Services.UI.Windows;
using Features.UI.Windows.Base;

namespace Features.Services.UI.Factory.BaseUI
{
  public interface IUIFactory : ICleanupService
  {
    event Action<WindowId,BaseWindow> Spawned;
    void CreateWindow(WindowId id, IWindowsService windowsService);
  }
}