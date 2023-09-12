using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionCanvas : MonoBehaviour
{
    private Canvas canvas;

    [SerializeField] Camera mainCamera;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        // ChangeRenderMode();
    }

    public void ChangeRenderMode()
    {
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = mainCamera;
        canvas.planeDistance = 1.0f;

        this.transform.localScale = new Vector3(0.0003f, 0.0003f, 0.0003f);
    }
}
