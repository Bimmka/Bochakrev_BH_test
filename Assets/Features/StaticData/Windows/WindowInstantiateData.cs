using System;
using Features.Services.UI.Factory;
using Features.UI.Windows.Base;

namespace Features.StaticData.Windows
{
  [Serializable]
  public struct WindowInstantiateData
  {
    public WindowId ID;
    public BaseWindow Window;
  }
}