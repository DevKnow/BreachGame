using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SceneManagerBase<TView, TInput> : MonoBehaviour
    where TView : MonoBehaviour
    where TInput : MonoBehaviour
{
    [Header("Core")]
    [SerializeField] protected TView _view;
    [SerializeField] protected TInput _inputHandler;

    protected virtual void Start()
    {
        EnterScene();
    }

    protected virtual void OnDestroy()
    {
        ExitScene();
    }

    public virtual void EnterScene()
    {
        _inputHandler.enabled = true;
    }

    public virtual void ExitScene()
    {
        _inputHandler.enabled = false;
    }
}
