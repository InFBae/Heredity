using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberPadlock : MonoBehaviour
{
    [SerializeField] private int[] password = new int[4];
    [SerializeField] NumberPadlockRuller[] rullers;

    public void CheckPassword()
    {
        for(int i = 0; i < password.Length; i++)
        {
            if (password[i] != rullers[i].GetNumber())
                return;
        }
        Unlock();
    }

    public void Unlock()
    {
        Debug.Log("NumberPadlock Unlocked");
    }
}

