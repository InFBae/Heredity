using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NumberPadlock : MonoBehaviour
{
    [SerializeField] private int[] password = new int[4];
    [SerializeField] NumberPadlockRuller[] rullers;

    [SerializeField] Joint[] ringJoint;
    [SerializeField] Collider ringCollider;

    public void CheckPassword()
    {
        for(int i = 0; i < password.Length; i++)
        {
            //Debug.Log($"Password : {password[i]}, Ruller : {rullers[i].GetNumber()}");
            if (password[i] != rullers[i].num)   
                return;
        }
        unlockedEvent?.Invoke();
    }

    [System.Serializable]
    public class UnlockedEvent : UnityEvent { }

    public UnlockedEvent unlockedEvent;

    public void Unlock()
    {
        Debug.Log("NumberPadlock Unlocked");
        foreach(Joint joint in ringJoint)
        {
            joint.breakForce = 0;
        }
        ringCollider.enabled = false;
    }
}

