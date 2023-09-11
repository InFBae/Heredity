using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    public LayerMask layer;
    public float maxDistance;

    private GameObject PasswordCanvas;

    [SerializeField] GameObject Light;

    private void Awake()
    {
        this.enabled = false;
    }

    private void OnEnable()
    {
        Light.SetActive(true);
    }

    private void OnDisable()
    {
        Light.SetActive(false);
    }
    private void Update()
    {
        RaycastHit hit;
        var hitPosition = Vector3.zero;

        if(Physics.Raycast(this.transform.position, this.transform.forward, out hit, maxDistance, layer))
        {
            PasswordCanvas = hit.transform.gameObject;
            PasswordCanvas.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            PasswordCanvas.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
