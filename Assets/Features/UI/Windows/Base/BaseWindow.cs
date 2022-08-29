using System;
using Features.Services.UI.Factory;
using UnityEngine;

namespace Features.UI.Windows.Base
{
  public class BaseWindow : MonoBehaviour
  {
    public event Action<BaseWindow> Destroyed;
    
    public WindowId ID { get; private set; }
    
    private void Awake() => 
      OnAwake();


    private void OnDestroy()
    {
      Cleanup();
      Destroyed?.Invoke(this);
    }

    protected virtual void Initialize() { }

    protected virtual void Subscribe() { }

    protected virtual void Cleanup() { }


    private void OnAwake()
    {
      Initialize();
      Subscribe();
    }

    public virtual void Open() => 
      gameObject.SetActive(true);

    public void SetID(WindowId id) => 
      ID = id;

    public virtual void Destroy()
    {
      Destroy(gameObject);
    }
  }
}