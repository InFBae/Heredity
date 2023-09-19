using Michsky.UI.ModernUIPack;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PotionChecker : MonoBehaviour
{
    MaterialPropertyBlock m_MaterialPropertyBlock;
    MeshRenderer m_Renderer;

    [SerializeField] private float fillAmount;

    private bool isFilled = false;


    private void Awake()
    {
        m_Renderer = GetComponent<MeshRenderer>();
    }

    private void OnEnable()
    {
        // ��ü ���� ���̴��� �����ϱ� ���� MaterialPropertyBlock ����
        m_MaterialPropertyBlock = new MaterialPropertyBlock();
        m_MaterialPropertyBlock.SetFloat("LiquidFill", fillAmount);

        // �Ž÷������� �Ӽ� ����
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
        // �ٸ� ������ ����ִ� �縸ŭ ���� ������ fillAmount�� ä����
        fillAmount += amount * Time.deltaTime;


        renderer.GetPropertyBlock(mpb);
        m_Renderer.GetPropertyBlock(m_MaterialPropertyBlock);

        // ����ִٸ� �״� ��ü�� ������ ä��
        if (!isFilled)
        {
            // ��ü�� �߾�, �����ڸ� ������ ���� (���̴� ����)
            m_MaterialPropertyBlock.SetColor("_MainLiquid", mpb.GetColor("_MainLiquid"));
            m_MaterialPropertyBlock.SetColor("_EdgeLiquid", mpb.GetColor("_EdgeLiquid"));
            isFilled = true;
        }

        renderer.SetPropertyBlock(mpb);
        m_Renderer.SetPropertyBlock(m_MaterialPropertyBlock);
        

        if (fillAmount > 0.9)
        {
            CheckPotion(liquidData);
        }
    }

    public void CheckPotion(LiquidData liquidData)
    {
        if (liquidData.liquidName == "Success Liquid")
        {
            // Success
            Debug.Log("Success");
        }
        else
        {
            // Fail
            Debug.Log("Fail");
        }
    }
}
