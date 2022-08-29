using System.Collections.Generic;
using Features.Services.UI.Factory;
using Features.Services.UI.Factory.BaseUI;
using Features.UI.Windows.Base;

namespace Features.Services.UI.Windows
{
  public class WindowsService : IWindowsService
  {
    private readonly IUIFactory uiFactory;

    private readonly Dictionary<WindowId, BaseWindow> windows;

    public bool IsCleanedUp { get; private set; }

    public WindowsService(IUIFactory uiFactory)
    {
      this.uiFactory = uiFactory;
      this.uiFactory.Spawned += AddSpawnedWindow;
   
      windows = new Dictionary<WindowId, BaseWindow>(10);
    }

    public void Cleanup()
    {
      IsCleanedUp = true;
      uiFactory.Spawned -= AddSpawnedWindow;
    }

    public void Open(WindowId windowId)
    {
      if (windows.ContainsKey(windowId) == false)
        CreateWindow(windowId);
    }

    public void Close(WindowId windowId)
    {
      if (windows.ContainsKey(windowId) == false)
        return;

      windows[windowId].Destroy();
    }

    private void CreateWindow(WindowId windowId) => 
      uiFactory.CreateWindow(windowId, this);


    private void AddSpawnedWindow(WindowId windowId, BaseWindow window)
    {
      windows.Add(windowId, window);
      window.Destroyed += OnWindowDestroyed;
    }

    private void OnWindowDestroyed(BaseWindow window)
    {
      window.Destroyed -= OnWindowDestroyed;
      Close(window.ID);
      windows.Remove(window.ID);
    }
  }
}