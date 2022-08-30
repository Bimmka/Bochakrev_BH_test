using Features.Services.EntityFactories;

namespace Features.Services.Network
{
  public interface INetwork : IService
  {
    void Construct(IHeroFactory heroFactory);
  }
}