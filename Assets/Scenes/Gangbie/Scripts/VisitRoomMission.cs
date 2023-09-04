using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class VisitRoomMission : MonoBehaviour
{
    [SerializeField] TMP_Text monitorText;

    private int socketSelectedCount = 0;


    [System.Serializable]
    public class MissionCompleteEvent : UnityEvent { }

    public MissionCompleteEvent missonCompleted;

    public void OnMissionComplete()
    {
        monitorText.text = "3�� �������� ���踦 ì�⼼��";
    }

    public void SocketSelectEntered()
    {
        socketSelectedCount++;

        if (socketSelectedCount == 4)
        {
            missonCompleted?.Invoke();
        }
    }

    public void SocketSelectExited()
    {
        socketSelectedCount--;
    }

}
