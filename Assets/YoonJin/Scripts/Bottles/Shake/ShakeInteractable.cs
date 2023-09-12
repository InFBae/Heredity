using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ShakeInteractable : XRBaseInteractable
{
    [SerializeField] private float shakeSpeed = 1.0f; // ��鸲�� ���� �ּ� �ӵ�
    [SerializeField] private int shakeCount = 10;     // ������ ���̱� ���� �ִ� Ƚ��

    private XRBaseInteractor interactor;
    private Rigidbody shakeRb;

    private float curShakeSpeed = 0; // ���� �ӵ�
    private int curShakeCount = 0;
    protected override void Awake()
    {
        base.Awake();
        shakeRb = GetComponent<Rigidbody>(); 
    }
    private void Start()
    {

    }

    public int ShakeCount
    {
        get { return shakeCount; }
    }

    // 실시간??
    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if (isSelected)
        {
            Debug.Log(shakeRb.velocity.magnitude);
            if(shakeRb.velocity.magnitude > shakeSpeed)
            {
                curShakeCount++;
            }
        }
    }


    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {

        interactor = args.interactorObject as XRBaseInteractor;

        /*
        base.OnSelectEntered(args);

        // ��鸲 �޼��� ȣ��
        Shake(args.interactor);
    }

    // ������Ʈ�� ��鸱 �� ȣ��Ǵ� �޼���
    public void Shake(XRBaseInteractor interactor)
    {
        Rigidbody interactorRigidbody = interactor.GetComponent<Rigidbody>();

        if (interactorRigidbody != null)
        {
            float shakeSpeed = interactorRigidbody.velocity.magnitude;

            if (curShakeSpeed >= shakeSpeed)
            {
                shakeCount++;
                Debug.Log(shakeCount + "ȸ ��鸲!");

                if (curShakeSpeed >=  && shakeCount >= fullyMixedCount)
                {
                    Debug.Log("������ �������ϴ�!");
                    // ������ ������ �� �߰� ó��
                }
            }
        } */
    }
}