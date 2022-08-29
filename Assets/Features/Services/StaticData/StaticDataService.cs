using System.Collections.Generic;
using System.Linq;
using Features.Constants;
using Features.Services.UI.Factory;
using Features.StaticData.Windows;
using UnityEngine;

namespace Features.Services.StaticData
{
  public class StaticDataService : IStaticDataService
  {
    private Dictionary<WindowId, WindowInstantiateData> windows;
    
    public void Load()
    {
      windows = Resources
        .Load<WindowsStaticData>(GameConstants.WindowsDataPath)
        .InstantiateData
        .ToDictionary(x => x.ID, x => x);

      Resources.UnloadUnusedAssets();
    }
    
    public WindowInstantiateData ForWindow(WindowId windowId) =>
      windows.TryGetValue(windowId, out WindowInstantiateData staticData)
        ? staticData 
        : new WindowInstantiateData();
  }
}