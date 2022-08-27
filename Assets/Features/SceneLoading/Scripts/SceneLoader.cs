using System;
using System.Collections;
using Features.Services.CoroutineRunner;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Features.SceneLoading.Scripts
{
  public class SceneLoader : ISceneLoader
  {
    private readonly ICoroutineRunner coroutineRunner;
    private readonly LoadingCurtain loadingCurtain;

    private Coroutine loadingCoroutine;

    public SceneLoader(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain)
    {
      this.coroutineRunner = coroutineRunner;
      this.loadingCurtain = loadingCurtain;
    }

    public void Load(string name, Action onLoaded, Action onCurtainHide)
    {
      if (loadingCoroutine != null)
        coroutineRunner.StopCoroutine(loadingCoroutine);
      loadingCoroutine = coroutineRunner.StartCoroutine(LoadScene(name, onLoaded, onCurtainHide));
    }
    
    public void Load(string name, Action onLoaded)
    {
      if (loadingCoroutine != null)
        coroutineRunner.StopCoroutine(loadingCoroutine);
      loadingCoroutine = coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));
    }

    private IEnumerator LoadScene(string nextScene, Action onLoaded = null, Action onCurtainHide= null)
    {
      if (SceneManager.GetActiveScene().name == nextScene)
      {
        onLoaded?.Invoke();
        yield break;
      }
      loadingCurtain.Show();
      while (loadingCurtain.IsShown == false)
        yield return null;
      
      AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);

      while (waitNextScene.isDone == false)
        yield return null;
      
      loadingCurtain.Hide();
      
      onLoaded?.Invoke();
      while (loadingCurtain.IsShown)
        yield return null;
     
      onCurtainHide?.Invoke();
      loadingCoroutine = null;
    }

    private IEnumerator LoadScene(string nextScene, Action onLoaded = null)
    {
      if (SceneManager.GetActiveScene().name == nextScene)
      {
        onLoaded?.Invoke();
        yield break;
      }

      AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);

      while (waitNextScene.isDone == false)
        yield return null;
      
      onLoaded?.Invoke();
      loadingCoroutine = null;
    }
  }
}
