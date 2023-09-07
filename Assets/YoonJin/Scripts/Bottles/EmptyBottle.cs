using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyBottle : MonoBehaviour
{
    public List<RecipeData> recipes = new List<RecipeData>();
    private List<LiquidData> inLiquidBottle = new List<LiquidData>();

    // ä�����ִ���
    private bool isFilled = false;
    // ȥ�չ�����
    private bool isMixed = false; 

    public void FillBottle(float amount, MeshRenderer renderer, MaterialPropertyBlock mpb, LiquidData liquidData)
    {
        LiquidBottle liquidBottle = GetComponent<LiquidBottle>();

        if (!liquidBottle.isPlugIn)
        {
            // �ٸ� ������ ����ִ� �縸ŭ ���� ������ fillAmount�� ä����
            liquidBottle.fillAmount += amount * Time.deltaTime;

            // empMpb, empRenderer�� ����ִ� ������ material
            // mpb, renderer�� ä�����ϴ� ������ material
            MaterialPropertyBlock empMpb = GetComponent<LiquidBottle>().m_MaterialPropertyBlock;
            MeshRenderer empRenderer = GetComponent<LiquidBottle>().mesh;

            renderer.GetPropertyBlock(mpb);
            empRenderer.GetPropertyBlock(empMpb);

            // ����ִٸ� �״� ��ü�� ������ ä��
            if (!isFilled)
            {
                // ��ü�� �߾�, �����ڸ� ������ ���� (���̴� ����)
                empMpb.SetColor("_MainLiquid", mpb.GetColor("_MainLiquid"));
                empMpb.SetColor("_EdgeLiquid", mpb.GetColor("_EdgeLiquid"));
                isFilled = true;
            }

            // ä�����ִٸ� ���� ���� ���� ���� ȥ���Ͽ� ���� ����
            if (mpb.GetColor("_MainLiquid") != empMpb.GetColor("_MainLiquid") && !isMixed)
            {
                empMpb.SetColor("_MainLiquid", mpb.GetColor("_MainLiquid") + empMpb.GetColor("_MainLiquid"));
                empMpb.SetColor("_EdgeLiquid", mpb.GetColor("_EdgeLiquid") + empMpb.GetColor("_EdgeLiquid"));
                isMixed = true;
            }

            renderer.SetPropertyBlock(mpb);
            empRenderer.SetPropertyBlock(empMpb);

            // �� �ȿ� �ش� ��ü�� ������� ���� ���
            if (!inLiquidBottle.Contains(liquidData))
            {
                inLiquidBottle.Add(liquidData); // ����Ʈ�� �ش� ��ü �����͸� �߰�
                Debug.Log(liquidData + "�� ����");

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

        // ������ ������ ����Ʈ�� ��ȸ�ϸ� �˻�
        foreach (RecipeData recipe in recipes)
        {
            // �������� ��ü �迭�� inLiquidList�� ��
            bool isMatch = true;
            foreach(LiquidData recipeIngredient in recipe.recipeIngredients)
            {
                // ���� �� ���� ����ִ� ��ü �����Ͱ� ������ �����Ϳ� ������ isMatch = false
                if (!inLiquidBottle.Contains(recipeIngredient))
                {
                    isMatch = false;
                    break;
                }
            }
            if (isMatch)
            {
                recipeData = recipe;
                // ��ü �����Ͱ� ��ġ�ϴ� �����Ǹ� ã���� ��, �ش� ������ ��ȯ
                return true;
            }
        }
        return false;   // ��ġ�ϴ� �����Ǹ� ã�� ���� ���
    }
}
