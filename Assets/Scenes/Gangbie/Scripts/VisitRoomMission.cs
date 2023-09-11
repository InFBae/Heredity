using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class VisitRoomMission : MonoBehaviour
{
    [SerializeField] TMP_Text monitorText;
    [SerializeField] Drawer drawer1;
    [SerializeField] Drawer drawer3;

    private int socketSelectedCount = 0;


    [System.Serializable]
    public class MissionCompleteEvent : UnityEvent { }

    public MissionCompleteEvent missonCompleted;

    public void OnMissionComplete()
    {
        monitorText.text = "뒤의 철제 사물함 서랍에서 열쇠를 챙기세요";
        drawer1.Unlock();
        drawer3.Unlock();
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
