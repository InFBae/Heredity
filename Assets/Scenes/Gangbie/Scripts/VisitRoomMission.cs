using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitRoomMission : MonoBehaviour
{
    [SerializeField] GameObject computerCanvas;

    [SerializeField] GameObject chair1;
    [SerializeField] GameObject chair2;
    [SerializeField] GameObject chair3;
    [SerializeField] GameObject chair4;

    [SerializeField] GameObject socket1;
    [SerializeField] GameObject socket2;
    [SerializeField] GameObject socket3;
    [SerializeField] GameObject socket4;

    private bool isMissionClear = false;

    private void Update()
    {
        if (chair1.transform.position == socket1.transform.position &&
            chair2.transform.position == socket2.transform.position &&
            chair3.transform.position == socket3.transform.position &&
            chair4.transform.position == socket4.transform.position)
        {
            isMissionClear = true;
        }
    }
}
