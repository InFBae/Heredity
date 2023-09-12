using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmpBottleContent : MonoBehaviour
{
    public RecipeData[] Recipes;
    // �� ��ü ������ ����Ʈ�� �ֱ� ���� ��ü ����Ʈ�� ��ü
    List<LiquidData> m_CurrentLiquidsIn = new List<LiquidData>();

    private void OnTriggerEnter(Collider other)
    {
        // �浹�� ��ü�� LiquidData ������Ʈ �޾ƿ���
        LiquidData data = other.GetComponent<LiquidData>();

        if (data != null)
        {
            // �ֱ� ���� ��ü ����Ʈ�� data�߰�
            if (!m_CurrentLiquidsIn.Contains(data))
            {
                m_CurrentLiquidsIn.Add(data);
            }
        }
        else
        {
            // m_CurrentLiquidsIn.Add();
            // 
        }
    }
}
