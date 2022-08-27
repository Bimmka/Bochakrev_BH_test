namespace Features.Services
{
  public interface IService
  {
  }

  public interface ICleanupService : IService
  {
    bool IsCleanedUp { get;}
    void Cleanup();
  }
}