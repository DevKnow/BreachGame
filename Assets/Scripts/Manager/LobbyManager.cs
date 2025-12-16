using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : SceneManagerBase<LobbyView, LobbyInputHandler>
{
    public override void EnterScene()
    {
        _view.Initialize();
        _inputHandler.enabled = true;
    }

    /// <summary>
    /// Called when exits lobby scene.
    /// </summary>
    public override void ExitScene()
    {
        _inputHandler.enabled = false;
    }
}
