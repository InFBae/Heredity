using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LiquidBottle : XRBaseInteractable
{
    public GameObject plugObj;  // 마개
    public ParticleSystem particleSystemLiquid;
    public ParticleSystem particleSystemSplash;
    public float fillAmount = 0.8f; // 초기 액체의 양

    [SerializeField] MeshRenderer mesh;
    [SerializeField] GameObject SmashedObject;  // 깨지는 효과

    Rigidbody plugRb;   // 뚜껑의 리지드바디
    MaterialPropertyBlock m_MaterialPropertyBlock;  // 셰이더의 속성 블록
    Rigidbody bottleRb;   // 병의 리지드바디

    private bool isPouring = false; // 현재 붓고있는지
    private bool isPlugIn = true;   // 현재 뚜껑이 꽂혀있는지
    bool isBreakable;   // 깨질 수 있는지의 여부
    float startingFillAmount;   // 초기 액체의 양

    private void OnEnable()
    {
        particleSystemLiquid.Stop();    // 파티클 재생 중지

        if (particleSystemSplash)
            particleSystemSplash.Stop();

        // 액체 양을 셰이더에 전달하기 위한 MaterialPropertyBlock 생성
        m_MaterialPropertyBlock = new MaterialPropertyBlock();
        m_MaterialPropertyBlock.SetFloat("LiquidFill", fillAmount);

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

    private void Update()
    {
        // 포션이 뚜껑이 없고, 액체 양이 0보다 크며, 위를 바라보고 있을 때
        if (Vector3.Dot(transform.up, Vector3.down) > 0 && fillAmount > 0 && isPlugIn == false)
        {
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
    }

    // 포션 깨짐 여부 설정
    public void ToggleBreakable(bool breakable)
    {
        isBreakable = breakable;
    }

    // 뚜껑 제거 함수
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
