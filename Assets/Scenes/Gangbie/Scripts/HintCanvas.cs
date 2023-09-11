using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HintCanvas : MonoBehaviour
{
    [SerializeField] TMP_Text hintText;

    string hint1 = "��ȸ�ǿ��� ���� 1�� ���Ƚ� ���踦 ȹ���� �� �ֽ��ϴ�";
    string hint2 = "����� ��������� ���⸦ �����غ�����";
    string hint3 = "��Ʈ 3";
    string hint4 = "��Ʈ 4";
    string hint5 = "��Ʈ 5";

    private void OnEnable()
    {
        int randNum = Random.Range(1, 6);

        if (randNum == 1 )
            hintText.text = hint1;
        else if (randNum == 2 )
            hintText.text = hint2;
        else if (randNum == 3)
            hintText.text = hint3;
        else if (randNum == 4)
            hintText.text = hint4;
        else if (randNum == 5)
            hintText.text = hint5;
    }
}
