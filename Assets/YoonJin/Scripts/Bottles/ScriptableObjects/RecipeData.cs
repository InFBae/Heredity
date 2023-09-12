using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "Data/RecipeData")]

public class RecipeData : ScriptableObject
{
    // 다른 클래스에서 변환 주의
    [Tooltip("레시피 데이터")]
    [SerializeField] public string recipeName; // 레시피 이름
    [SerializeField] public Sprite recipeSprite; // 레시피 스프라이트
    [SerializeField] public int mixCount;   // 필요 회전수
    [SerializeField] public LiquidData[] recipeIngredients;  // 재료가 되는 액체데이터 배열
    [SerializeField, TextArea] public string toolTip;

}
