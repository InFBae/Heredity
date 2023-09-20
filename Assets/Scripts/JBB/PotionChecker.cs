using Michsky.UI.ModernUIPack;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Events;

public class PotionChecker : MonoBehaviour
{
    [System.Serializable]
    public class SuccessEvent : UnityEvent { }

    MaterialPropertyBlock m_MaterialPropertyBlock;
    MeshRenderer m_Renderer;
    LiquidData m_LiquidData;

    [SerializeField] private float fillAmount;
    [SerializeField] GameObject endingPoint;
    [SerializeField] XROrigin player;
    [SerializeField] Animator animator;

    [SerializeField] private BGMController bgmController;
    [SerializeField] Canvas endingUI;

    public SuccessEvent successEvent;

    private bool isFilled = false;
    int stepChanged = 0;


    private void Awake()
    {
        m_Renderer = GetComponent<MeshRenderer>();
    }

    private void OnEnable()
    {
        // 액체 양을 셰이더에 전달하기 위한 MaterialPropertyBlock 생성
        m_MaterialPropertyBlock = new MaterialPropertyBlock();
        m_MaterialPropertyBlock.SetFloat("LiquidFill", fillAmount);

        // 매시랜더러의 속성 설정
        m_Renderer.SetPropertyBlock(m_MaterialPropertyBlock);
    }

    private void Update()
    {
        m_Renderer.GetPropertyBlock(m_MaterialPropertyBlock);
        m_MaterialPropertyBlock.SetFloat("LiquidFill", fillAmount);
        m_Renderer.SetPropertyBlock(m_MaterialPropertyBlock);
    }

    public void FillFunnel(float amount, MeshRenderer renderer, MaterialPropertyBlock mpb, LiquidData liquidData)
    {
        // 다른 물병에 들어있는 양만큼 현재 물병의 fillAmount를 채워줌
        fillAmount += amount * Time.deltaTime;


        renderer.GetPropertyBlock(mpb);
        m_Renderer.GetPropertyBlock(m_MaterialPropertyBlock);

        // 비어있다면 붓는 액체의 색으로 채움
        if (!isFilled)
        {
            // 액체의 중앙, 가장자리 색상을 변경 (쉐이더 참조)
            m_MaterialPropertyBlock.SetColor("_MainLiquid", mpb.GetColor("_MainLiquid"));
            m_MaterialPropertyBlock.SetColor("_EdgeLiquid", mpb.GetColor("_EdgeLiquid"));
            m_LiquidData = liquidData;
            isFilled = true;
        }

        renderer.SetPropertyBlock(mpb);
        m_Renderer.SetPropertyBlock(m_MaterialPropertyBlock);
        
    }

    public void CheckPotion(LiquidData liquidData)
    {
        if (fillAmount > 0.8f && liquidData.liquidName == "Success Liquid")
        {
            // Success
            Debug.Log("Success");
            successEvent.Invoke();
            StartCoroutine(GameEndRoutine());
            bgmController.ChangeBGM("BGM/Hopeful/endBGM");
        }
        else
        {
            // Fail
            Debug.Log("Fail");
        }
        StartCoroutine(ReduceFillAmountRoutine());
    }

    public void DialCheck(int step)
    {
        //Debug.Log($"step : {step}");
        stepChanged++;
        if (stepChanged > 5)
        {
            stepChanged = 0;
            CheckPotion(m_LiquidData);
        }
    }

    IEnumerator ReduceFillAmountRoutine()
    {
        while (true)
        {
            fillAmount -= 0.05f * Time.deltaTime;
            yield return null;

            if (fillAmount < 0.5f)
            {
                yield break;
            }
        }
    }

    IEnumerator GameEndRoutine()
    {
        yield return new WaitForSeconds(3f);
        player.transform.position = endingPoint.transform.position;
        player.transform.rotation = endingPoint.transform.rotation;
        animator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(3f);
        endingUI.gameObject.SetActive(true);
    }
}
