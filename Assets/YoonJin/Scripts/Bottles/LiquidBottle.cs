using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class LiquidBottle : RespawnableBottle
{
    [SerializeField] public string prefabName; // 병 프리팹
    public GameObject plugObj;  // 마개
    public ParticleSystem particleSystemLiquid;
    public ParticleSystem particleSystemSplash;
    public float fillAmount = 0.8f; // 초기 액체의 양

    [SerializeField] public MeshRenderer mesh;
    [SerializeField] GameObject SmashedObject;  // 깨지는 효과

    Rigidbody plugRb;   // 뚜껑의 리지드바디
    public MaterialPropertyBlock m_MaterialPropertyBlock;  // 셰이더의 속성 블록
    Rigidbody bottleRb;   // 병의 리지드바디
    [SerializeField] public LiquidData liquidData;  // 병 안에 들어있는 액체의 정보

    private bool isPouring = false; // 현재 붓고있는지
    public bool isPlugIn = true;   // 현재 뚜껑이 꽂혀있는지
    bool isBreakable;   // 깨질 수 있는지의 여부
    float startingFillAmount;   // 초기 액체의 양

    [SerializeField] XRExclusiveSocketInteractor soInteractor;

    [SerializeField] Color color;

    public UnityEvent pourAudio;
    public UnityEvent splashAudio;
    public UnityEvent breakAudio;

    private void OnEnable()
    {
        particleSystemLiquid.Stop();    // 파티클 재생 중지

        if (particleSystemSplash)
            particleSystemSplash.Stop();

        // 액체 양을 셰이더에 전달하기 위한 MaterialPropertyBlock 생성
        m_MaterialPropertyBlock = new MaterialPropertyBlock();
        m_MaterialPropertyBlock.SetFloat("LiquidFill", fillAmount);
        m_MaterialPropertyBlock.SetColor("_MainLiquid", color);
        m_MaterialPropertyBlock.SetColor("_EdgeLiquid", color);

        // 매시랜더러의 속성 설정
        mesh.SetPropertyBlock(m_MaterialPropertyBlock);

        // 뚜껑과 병의 리지드바디 가져오기
        plugRb = plugObj.GetComponent<Rigidbody>();
        bottleRb = GetComponent<Rigidbody>();

        // 초기 액체 양 저장
        startingFillAmount = fillAmount;

        // 병을 깨질 수 있게설정
        isBreakable = true;
    }

    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        // 포션이 뚜껑이 없고, 액체 양이 0보다 크며, 위를 바라보고 있을 때
        if (Vector3.Dot(transform.up, Vector3.down) > 0 && fillAmount > 0 && isPlugIn == false)
        {
            pourAudio?.Invoke();
            // 파티클 재생
            if (particleSystemLiquid.isStopped)
            {
                particleSystemLiquid.Play();
            }

            // 액체 양 감소
            fillAmount -= 0.1f * Time.deltaTime;

            // 액체가 바닥에 닿았을 때, 콜라이더 처리
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

                // 들어가야 하는 병에 들어갔을 때
                // LiquidReceiver receiver = hit.collider.GetComponent<LiquidReceiver>();
                
                // 아닐 때
                // 리셋처리 (다시 포션 생성)
            }
        }
        else
        {
            // 파티클 시스템 정지
            particleSystemLiquid.Stop();
            
        }
        mesh.GetPropertyBlock(m_MaterialPropertyBlock);
        m_MaterialPropertyBlock.SetFloat("LiquidFill", fillAmount);
        mesh.SetPropertyBlock(m_MaterialPropertyBlock);

        // 소켓 인터렉터가 선택됐을때(True일때) isPlugIn = true가 된다
        isPlugIn = soInteractor.hasSelection;
    }

    // 포션 깨짐 여부 설정
    public void ToggleBreakable(bool breakable)
    {
        isBreakable = breakable;
    }

    // 뚜껑 제거 함수
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
  

    // 충돌과 병 깨짐 함수
    private void OnCollisionEnter(Collision collision)
    {
        if (isBreakable && bottleRb.velocity.magnitude > 1.35)
        {
            // 마개가 꽂혀있을 때
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
