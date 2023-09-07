using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;

[CreateAssetMenu(fileName = "LiquidData", menuName = "Data/LiquidData")]
public class LiquidData : ScriptableObject
{
    // �ٸ� Ŭ�������� ��ȯ����
    [Tooltip("��ü ������")]
    [SerializeField] public Sprite liquidSprite;
    [SerializeField] public string liquidName;
    [SerializeField, TextArea] public string toolTip;
}
