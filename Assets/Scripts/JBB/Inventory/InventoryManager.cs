using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventory;
    public GameObject Anchor;
    bool UIActive;
    [SerializeField] XRBaseInteractor leftHand;
    [SerializeField] XRBaseInteractor rightHand;

    private void OnEnable()
    {
        //rightHand.selectEntered.AddListener(OnSelectEntered);
    }

    private void OnDisable()
    {
        //rightHand.selectEntered.RemoveListener(OnSelectEntered);
    }

    private void Start()
    {
        DeactivateUI();
    }

    private void Update()
    {               
        if (UIActive)
        {
            inventory.transform.position = Anchor.transform.position;
            inventory.transform.eulerAngles = new Vector3(Anchor.transform.eulerAngles.x + 15, Anchor.transform.eulerAngles.y, 0);
        }
    }

    private void OnPrimaryButton(InputValue inputValue)
    {
        if (leftHand.interactablesSelected.Count > 0)
        {
            return;
        }
        if (inputValue.isPressed)
        {
            ActivateUI();
        }
        else
        {
            DeactivateUI();
        }
    }

    private void ActivateUI()
    {
        UIActive = true;
        inventory.SetActive(true);
    }

    private void DeactivateUI()
    {
        UIActive = false;
        inventory.SetActive(false);
    }
    /*
    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        XRBaseInteractable grabbed = args.interactableObject as XRBaseInteractable;
        GameObject go = grabbed.gameObject;
        if (go.GetComponent<Item>() == null)
            return;
        if (go.GetComponent<Item>().inSlot)
        {
            go.GetComponent<Rigidbody>().isKinematic = false;
            go.GetComponent<Item>().currentSlot.ItemInSlot = null;
            go.transform.SetParent(null);
            go.transform.localScale = go.GetComponent<Item>().originalScale;
            go.GetComponent<Item>().inSlot = false;
            go.GetComponent<Item>().currentSlot.ResetColor();
            go.GetComponent<Item>().currentSlot = null;
        }
    }*/
}