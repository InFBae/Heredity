using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LiquidBottle : XRBaseInteractable
{
    public GameObject plugObj;  // ����
    public ParticleSystem particleSystemLiquid;
    public ParticleSystem particleSystemSplash;
    public float fillAmount = 0.8f; // �ʱ� ��ü�� ��

    [SerializeField] MeshRenderer mesh;
    [SerializeField] GameObject SmashedObject;  // ������ ȿ��

    Rigidbody plugRb;   // �Ѳ��� ������ٵ�
    MaterialPropertyBlock m_MaterialPropertyBlock;  // ���̴��� �Ӽ� ���
    Rigidbody bottleRb;   // ���� ������ٵ�

    private bool isPouring = false; // ���� �װ��ִ���
    private bool isPlugIn = true;   // ���� �Ѳ��� �����ִ���
    bool isBreakable;   // ���� �� �ִ����� ����
    float startingFillAmount;   // �ʱ� ��ü�� ��

    private void OnEnable()
    {
        particleSystemLiquid.Stop();    // ��ƼŬ ��� ����

        if (particleSystemSplash)
            particleSystemSplash.Stop();

        // ��ü ���� ���̴��� �����ϱ� ���� MaterialPropertyBlock ����
        m_MaterialPropertyBlock = new MaterialPropertyBlock();
        m_MaterialPropertyBlock.SetFloat("LiquidFill", fillAmount);

        // �Ž÷������� �Ӽ� ����
        mesh.SetPropertyBlock(m_MaterialPropertyBlock);

        // �Ѳ��� ���� ������ٵ� ��������
        plugRb = plugObj.GetComponent<Rigidbody>();
        bottleRb = GetComponent<Rigidbody>();

        // �ʱ� ��ü �� ����
        startingFillAmount = fillAmount;

        // ���� ���� �� �ְԼ���
        isBreakable = true;
    }

    private void Update()
    {
        // ������ �Ѳ��� ����, ��ü ���� 0���� ũ��, ���� �ٶ󺸰� ���� ��
        if (Vector3.Dot(transform.up, Vector3.down) > 0 && fillAmount > 0 && isPlugIn == false)
        {
            // ��ƼŬ ���
            if (particleSystemLiquid.isStopped)
            {
                particleSystemLiquid.Play();
            }

            // ��ü �� ����
            fillAmount -= 0.1f * Time.deltaTime;

            // ��ü�� �ٴڿ� ����� ��, �ݶ��̴� ó��
            RaycastHit hit;

            if (Physics.Raycast(particleSystemLiquid.transform.position, Vector3.down, out hit, 50.0f, ~0, QueryTriggerInteraction.Collide))
            {
                // ���� �ϴ� ���� ���� ��
                // LiquidReceiver receiver = hit.collider.GetComponent<LiquidReceiver>();
                
                // �ƴ� ��
                // ����ó�� (�ٽ� ���� ����)
            }
        }
        else
        {
            // ��ƼŬ �ý��� ����
            particleSystemLiquid.Stop();
        }
        mesh.GetPropertyBlock(m_MaterialPropertyBlock);
        m_MaterialPropertyBlock.SetFloat("LiquidFill", fillAmount);
        mesh.SetPropertyBlock(m_MaterialPropertyBlock);
    }

    // ���� ���� ���� ����
    public void ToggleBreakable(bool breakable)
    {
        isBreakable = breakable;
    }

    // �Ѳ� ���� �Լ�
    public void PlugOff()
    {
        if (isPlugIn)
        {
            isPlugIn = false;
            plugRb.transform.SetParent(null);
            plugRb.isKinematic = false;
            plugRb.AddRelativeForce(new Vector3(0, 0, 120));

            plugObj.transform.parent = null;
        }
    }

    // �浹�� �� ���� �Լ�
    private void OnCollisionEnter(Collision collision)
    {
        if (isBreakable && bottleRb.velocity.magnitude > 1.35)
        {
            // ������ �������� ��
            if (isPlugIn)
            {
                plugRb.isKinematic = false;
                plugObj.transform.parent = null;

                Collider c;
                if (plugObj.TryGetComponent(out c))
                    c.enabled = true;

                Destroy(plugObj, 4.0f);
            }

            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }

            if (particleSystemSplash != null)
            {
                particleSystemSplash.gameObject.SetActive(true);
                if (fillAmount > 0)
                {
                    particleSystemSplash.Play();
                }
            }

            SmashedObject.SetActive(true);

            Rigidbody[] rbs = SmashedObject.GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody rb in rbs)
            {
                rb.AddExplosionForce(100.0f, SmashedObject.transform.position, 2.0f, 15.0F);
            }

            Destroy(SmashedObject, 4.0f);
            Destroy(this);
        }
    }
}
