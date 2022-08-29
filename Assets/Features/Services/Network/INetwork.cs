using Features.Services.EntityFactories;
using Features.Services.UI.Windows;

namespace Features.Services.Network
{
  public interface INetwork : IService
  {
    void Construct(IHeroFactory heroFactory, IWindowsService windowsService);
  }
}