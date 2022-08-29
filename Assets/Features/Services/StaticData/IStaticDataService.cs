using Features.Services.UI.Factory;
using Features.StaticData.Windows;

namespace Features.Services.StaticData
{
  public interface IStaticDataService : IService
  {
    void Load();
    WindowInstantiateData ForWindow(WindowId id);
  }
}