using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNextScene : MonoBehaviour
{
    [SerializeField]protected InputManager _inputManager;

    [SerializeField]private string nextSceneName;

    // Update is called once per frame
    void Update()
    {
        if (_inputManager.JumpButton.State.CurrentState == MMInput.ButtonStates.ButtonDown)
        {
            MMSceneLoadingManager.LoadScene(nextSceneName);
        }
    }
}
