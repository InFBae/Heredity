using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyBottle : MonoBehaviour
{
    public List<RecipeData> recipes = new List<RecipeData>();
    private List<LiquidData> inLiquidBottle = new List<LiquidData>();

    // 채워져있는지
    private bool isFilled = false;
    // 혼합물인지
    private bool isMixed = false; 

    public void FillBottle(float amount, MeshRenderer renderer, MaterialPropertyBlock mpb, LiquidData liquidData)
    {
        LiquidBottle liquidBottle = GetComponent<LiquidBottle>();

        if (!liquidBottle.isPlugIn)
        {
            // 다른 물병에 들어있는 양만큼 현재 물병의 fillAmount를 채워줌
            liquidBottle.fillAmount += amount * Time.deltaTime;

            // empMpb, empRenderer는 비어있는 물병의 material
            // mpb, renderer는 채워야하는 물병의 material
            MaterialPropertyBlock empMpb = GetComponent<LiquidBottle>().m_MaterialPropertyBlock;
            MeshRenderer empRenderer = GetComponent<LiquidBottle>().mesh;

            renderer.GetPropertyBlock(mpb);
            empRenderer.GetPropertyBlock(empMpb);

            // 비어있다면 붓는 액체의 색으로 채움
            if (!isFilled)
            {
                // 액체의 중앙, 가장자리 색상을 변경 (쉐이더 참조)
                empMpb.SetColor("_MainLiquid", mpb.GetColor("_MainLiquid"));
                empMpb.SetColor("_EdgeLiquid", mpb.GetColor("_EdgeLiquid"));
                isFilled = true;
            }

            // 채워져있다면 현재 색과 원본 색을 혼합하여 색을 변경
            if (mpb.GetColor("_MainLiquid") != empMpb.GetColor("_MainLiquid") && !isMixed)
            {
                empMpb.SetColor("_MainLiquid", mpb.GetColor("_MainLiquid") + empMpb.GetColor("_MainLiquid"));
                empMpb.SetColor("_EdgeLiquid", mpb.GetColor("_EdgeLiquid") + empMpb.GetColor("_EdgeLiquid"));
                isMixed = true;
            }

            renderer.SetPropertyBlock(mpb);
            empRenderer.SetPropertyBlock(empMpb);

            // 병 안에 해당 액체가 들어있지 않을 경우
            if (!inLiquidBottle.Contains(liquidData))
            {
                inLiquidBottle.Add(liquidData); // 리스트에 해당 액체 데이터를 추가
                Debug.Log(liquidData + "를 넣음");

                if (CheckBottleMix(out RecipeData recipeData))
                {
                    Debug.Log(recipeData.recipeName + "is Success Making");
                }
            }
        }
    }

    private bool CheckBottleMix(out RecipeData recipeData)
    {
        recipeData = null;

        // 레시피 데이터 리스트를 순회하며 검사
        foreach (RecipeData recipe in recipes)
        {
            // 레시피의 액체 배열과 inLiquidList를 비교
            bool isMatch = true;
            foreach(LiquidData recipeIngredient in recipe.recipeIngredients)
            {
                // 현재 빈 병에 들어있는 액체 데이터가 레시피 데이터에 없으면 isMatch = false
                if (!inLiquidBottle.Contains(recipeIngredient))
                {
                    isMatch = false;
                    break;
                }
            }
            if (isMatch)
            {
                recipeData = recipe;
                // 액체 데이터가 일치하는 레시피를 찾았을 때, 해당 레시피 반환
                return true;
            }
        }
        return false;   // 일치하는 레시피를 찾지 못한 경우
    }
}
