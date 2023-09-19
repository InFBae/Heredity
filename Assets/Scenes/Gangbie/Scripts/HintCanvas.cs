using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HintCanvas : MonoBehaviour
{
    [SerializeField] Hint hint;

    [SerializeField] TMP_Text hintCount;
    [SerializeField] TMP_Text hintText;

    string hint1 = "���� ���ڸ� �����ϰ� ��ǻ�� ȭ���� Ȯ���ϼ���.";
    string hint2 = "��������� ���⸦ ������ �� ������� �� ���� Ȯ���ϼ���.";
    string hint3 = "�� ������ ������ �� ���� ��� ��� ��� ������ �ϼ��ϼ���.";
    string hint4 = "ǻ� �������ܱ⿡ �ְ� ������ ���� ������ �����ϼ���.";
    string hint5 = "������ �������̸� ���̺� ���� �л��� �̼��� Ȯ���ϼ���.";

    private void OnEnable()
    {
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
    }
}
