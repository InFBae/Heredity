using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    public LayerMask layer;
    public float maxDistance;

    private GameObject PasswordCanvas;
    private GameObject PrevPasswordCanvas;

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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(this.transform.position, this.transform.forward * maxDistance);
    }

}
