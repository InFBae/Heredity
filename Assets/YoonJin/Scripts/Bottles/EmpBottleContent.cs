using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmpBottleContent : MonoBehaviour
{
    public RecipeData[] Recipes;
    // 새 액체 데이터 리스트를 최근 넣은 액체 리스트로 대체
    List<LiquidData> m_CurrentLiquidsIn = new List<LiquidData>();

    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 물체의 LiquidData 컴포넌트 받아오기
        LiquidData data = other.GetComponent<LiquidData>();

        if (data != null)
        {
            // 최근 넣은 액체 리스트에 data추가
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
