using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;

[CreateAssetMenu(fileName = "LiquidData", menuName = "Data/LiquidData")]
public class LiquidData : ScriptableObject
{
    // 다른 클래스에서 변환주의
    [Tooltip("액체 데이터")]
    [SerializeField] public Sprite liquidSprite;
    [SerializeField] public string liquidName;
    [SerializeField, TextArea] public string toolTip;
}
