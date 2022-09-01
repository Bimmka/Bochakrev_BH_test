using Mirror;

namespace Features.Services.Network
{
  public readonly struct NetworkHeroModel : NetworkMessage
  {
    public readonly int ModelID;

    public NetworkHeroModel(int modelID)
    {
      ModelID = modelID;
    }
  }
}