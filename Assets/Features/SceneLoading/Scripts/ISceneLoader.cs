using System;

namespace Features.SceneLoading.Scripts
{
  public interface ISceneLoader
  {
    void Load(string name, Action onLoaded = null, Action onCurtainHide = null);
    void Load(string name, Action onLoaded);
  }
}