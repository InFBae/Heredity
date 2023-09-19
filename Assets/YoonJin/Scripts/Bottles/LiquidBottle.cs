using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class LiquidBottle : RespawnableBottle
{
    [SerializeField] public string prefabName; // �� ������
    public GameObject plugObj;  // ����
    public ParticleSystem particleSystemLiquid;
    public ParticleSystem particleSystemSplash;
    public float fillAmount = 0.8f; // �ʱ� ��ü�� ��

    [SerializeField] public MeshRenderer mesh;
    [SerializeField] GameObject SmashedObject;  // ������ ȿ��

    Rigidbody plugRb;   // �Ѳ��� ������ٵ�
    public MaterialPropertyBlock m_MaterialPropertyBlock;  // ���̴��� �Ӽ� ���
    Rigidbody bottleRb;   // ���� ������ٵ�
    [SerializeField] public LiquidData liquidData;  // �� �ȿ� ����ִ� ��ü�� ����

    private bool isPouring = false; // ���� �װ��ִ���
    public bool isPlugIn = true;   // ���� �Ѳ��� �����ִ���
    bool isBreakable;   // ���� �� �ִ����� ����
    float startingFillAmount;   // �ʱ� ��ü�� ��

    [SerializeField] XRExclusiveSocketInteractor soInteractor;

    [SerializeField] Color color;

    public UnityEvent pourAudio;
    public UnityEvent splashAudio;
    public UnityEvent breakAudio;

    private void OnEnable()
    {
        particleSystemLiquid.Stop();    // ��ƼŬ ��� ����

        if (particleSystemSplash)
            particleSystemSplash.Stop();

        // ��ü ���� ���̴��� �����ϱ� ���� MaterialPropertyBlock ����
        m_MaterialPropertyBlock = new MaterialPropertyBlock();
        m_MaterialPropertyBlock.SetFloat("LiquidFill", fillAmount);
        m_MaterialPropertyBlock.SetColor("_MainLiquid", color);
        m_MaterialPropertyBlock.SetColor("_EdgeLiquid", color);

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

    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        // ������ �Ѳ��� ����, ��ü ���� 0���� ũ��, ���� �ٶ󺸰� ���� ��
        if (Vector3.Dot(transform.up, Vector3.down) > 0 && fillAmount > 0 && isPlugIn == false)
        {
            pourAudio?.Invoke();
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
                EmptyBottle bottle = hit.collider.GetComponent<EmptyBottle>();

                if (bottle != null)
                {
                    bottle.FillBottle(0.1f, mesh, m_MaterialPropertyBlock, liquidData);
                }

                PotionChecker potionChecker = hit.collider.GetComponent<PotionChecker>();

                if (potionChecker != null)
                {
                    potionChecker.FillFunnel(0.1f, mesh, m_MaterialPropertyBlock, liquidData);
                }

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

        // ���� ���ͷ��Ͱ� ���õ�����(True�϶�) isPlugIn = true�� �ȴ�
        isPlugIn = soInteractor.hasSelection;
    }

    // ���� ���� ���� ����
    public void ToggleBreakable(bool breakable)
    {
        isBreakable = breakable;
    }

    // �Ѳ� ���� �Լ�
    public void PlugSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactorObject.transform.tag == "Player")
        {
            if (isPlugIn)
            {
                isPlugIn = false;
                plugRb.isKinematic = false;
                StartCoroutine(SocketReActive());
            }
        }
        
    }

    IEnumerator SocketReActive()
    {
        soInteractor.socketActive = false;
        yield return new WaitForSeconds(1f);
        soInteractor.socketActive = true;
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

                Destroy(plugObj.gameObject, 4.0f);
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
            breakAudio?.Invoke();
            Destroy(SmashedObject.gameObject, 4.0f);
            Destroy(this.gameObject, 4);
            GameManager.Resource.Instantiate<GameObject>(GameManager.Resource.Load<GameObject>($"Interactables/Potions/{prefabName}"), startingPosition, startingRotation);
            Collider[] colliders = GetComponentsInChildren<Collider>();
            foreach(Collider collider in colliders)
            {
                collider.enabled = false;
            }
        }
    }
}
