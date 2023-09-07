using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeaterLever : MonoBehaviour
{
    ParticleSystem particle;

    public int level;

    [SerializeField] GameObject pw1;
    [SerializeField] GameObject pw2;
    [SerializeField] GameObject pw3;
    [SerializeField] GameObject pw4;

    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }

    public void ChangeScale(int step)
    {
        particle.startLifetime = 12 - level * step;
        Debug.Log(particle.startLifetime);

        if (particle.startLifetime < 3)
        {
            pw1.SetActive(true);
            pw2.SetActive(true);
            pw3.SetActive(true);
            pw4.SetActive(true);
        }
    }
}
