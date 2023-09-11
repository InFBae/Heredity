using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HintCanvas : MonoBehaviour
{
    [SerializeField] Hint hint;

    [SerializeField] TMP_Text hintCount;
    [SerializeField] TMP_Text hintText;

    string hint1 = "��ȸ�ǿ��� ���� 1�� ���Ƚ� ���踦 ȹ���� �� �ֽ��ϴ�";
    string hint2 = "";
    string hint3 = "����� ��������� ���⸦ �����غ�����";
    string hint4 = "��Ʈ 4";
    string hint5 = "��Ʈ 5";
    string hint6 = "��Ʈ 6";

    private void OnEnable()
    {
        // int randNum = Random.Range(1, 6);
        hintCount.text = "���� ��Ʈ : " + hint.leftHintCount.ToString();

        if (hint.triggerNum == 1 )
            hintText.text = hint1;
        else if (hint.triggerNum == 2 )
            hintText.text = hint2;
        else if (hint.triggerNum == 3)
            hintText.text = hint3;
        else if (hint.triggerNum == 4)
            hintText.text = hint4;
        else if (hint.triggerNum == 5)
            hintText.text = hint5;
        else if (hint.triggerNum == 6)
            hintText.text = hint6;
    }
}
