using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "Data/RecipeData")]

public class RecipeData : ScriptableObject
{
    // �ٸ� Ŭ�������� ��ȯ ����
    [Tooltip("������ ������")]
    [SerializeField] public string recipeName; // ������ �̸�
    [SerializeField] public Sprite recipeSprite; // ������ ��������Ʈ
    [SerializeField] public int mixCount;   // �ʿ� ȸ����
    [SerializeField] public LiquidData[] recipeIngredients;  // ��ᰡ �Ǵ� ��ü������ �迭
    [SerializeField, TextArea] public string toolTip;

}
