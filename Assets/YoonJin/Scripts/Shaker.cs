using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Shaker : MonoBehaviour
{
    [SerializeField] private float shakeSpeed = 1f;
    [SerializeField] private int shakeCount = 10;     

    private Rigidbody shakeRb;

    private int curShakeCount = 0;

    private bool isWaiting = false;

    private void Awake()
    {
        shakeRb = GetComponent<Rigidbody>();  
    }


    private void Update()
    {
        // Debug.Log(shakeRb.velocity.magnitude);
        if (!isWaiting && shakeRb.velocity.magnitude > shakeSpeed)
        {
            curShakeCount++;
            isWaiting = true;
            StartCoroutine(WaitAndShake(0.5f));
            Debug.Log(curShakeCount + "È¸ ¼¯ÀÓ");


            if (curShakeCount >= shakeCount)
            {
                Debug.Log("¿Ï¼º!");
            }
        }
    }
    
    private IEnumerator WaitAndShake(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        isWaiting = false;
    }
    
    public void EnableShaker()
    {
        this.enabled = true;
    }

    public void DisableShaker()
    {
        this.enabled = false;
    }
}