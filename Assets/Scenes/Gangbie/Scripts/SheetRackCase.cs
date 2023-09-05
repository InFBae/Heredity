using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SheetRackCase : MonoBehaviour
{
    [SerializeField] GameObject jackInTheBox;
    [SerializeField] GameObject position;

    private void Update()
    {
        if (Vector3.Distance(this.transform.position, position.transform.position) < 0.01f)
        {
            // jackInTheBox.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            jackInTheBox.SetActive(true);
        }
    }
}
