using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    public UnityEvent OnStartSceneEntered;
    public UnityEvent OnStartSceneExited;

    private void OnEnable()
    {
        OnStartSceneEntered?.Invoke();
    }

    private void OnDisable()
    {
        OnStartSceneExited?.Invoke();
    }

    public void StartButton()
    {
        // SceneManager.LoadScene("GameScene");
    }
}
