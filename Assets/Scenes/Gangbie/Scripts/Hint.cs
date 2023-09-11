using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hint : Item, IUseable
{
    [SerializeField] GameObject hintCanvas;
    [SerializeField] List<GameObject> hintRandomPositions;
    [SerializeField] GameObject hintSocket;

    private int hintCount = 5;

    public void Use()
    {
        StartCoroutine(hintRoutine());
    }

    IEnumerator hintRoutine()
    {
        int randNum = Random.Range(0, 5);

        hintCanvas.SetActive(true);
        yield return new WaitForSeconds(5);
        hintCanvas.SetActive(false);
        hintSocket.SetActive(false);
        this.transform.position = hintRandomPositions[randNum].transform.position;
        hintSocket.SetActive(true);
    }
}
