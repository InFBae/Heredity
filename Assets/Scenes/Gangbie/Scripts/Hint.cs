using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class Hint : Item, IUseable
{
    [SerializeField] GameObject hintCanvas;
    [SerializeField] List<GameObject> hintColliders;
    public UnityEvent hintSound;

    public LayerMask hintLayer;

    private int hintCount = 4;
    public int leftHintCount { get { return hintCount - 1; } }

    public int triggerNum;

    public void Use()
    {
        if (hintCount > 0)
        {
            StartCoroutine(hintRoutine());
            hintCount--;
        }
    }

    IEnumerator hintRoutine()
    {
        hintCanvas.SetActive(true);
        yield return new WaitForSeconds(5);
        hintCanvas.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.layer == hintColliders[0].layer)
        {
            hintSound?.Invoke();
            if (other.gameObject == hintColliders[0])
            {
                triggerNum = 1;
                Use();
                hintColliders[0].SetActive(false);
            }
            else if (other.gameObject == hintColliders[1])
            {
                triggerNum = 2;
                Use();
                hintColliders[1].SetActive(false);
            }
            else if (other.gameObject == hintColliders[2])
            {
                triggerNum = 3;
                Use();
                hintColliders[2].SetActive(false);
            }
            else if (other.gameObject == hintColliders[3])
            {
                triggerNum = 4;
                Use();
                hintColliders[3].SetActive(false);
            }
            else if (other.gameObject == hintColliders[4])
            {
                triggerNum = 5;
                Use();
                hintColliders[4].SetActive(false);
            }
        }
    }

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        if (inSlot)
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            currentSlot.ItemInSlot = null;
            transform.SetParent(null);
            transform.localScale = originalScale;
            inSlot = false;
            gameObject.layer = LayerMask.NameToLayer("Hint");
            currentSlot.ResetColor();
            currentSlot = null;
        }

        base.OnSelectEntering(args);
    }
}
