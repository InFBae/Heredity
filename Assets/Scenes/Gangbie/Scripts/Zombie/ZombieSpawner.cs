using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    private readonly List<Zombie> zombies = new List<Zombie>();

    public Zombie zombiePrefab;

    [SerializeField] Transform[] spawnPoints;

    private void Awake()
    {
        SpawnWave();
    }

    public void SpawnWave()
    {
        int spawnCount = 6;
        for (int i = 0; i < spawnCount; i++)
        {
            CreateZombie(i);
        }
    }

    private void CreateZombie(int point)
    {
        float moveSpeed = 2;
        float runSpeed = 3;

        var spawnPoint = spawnPoints[point];

        var zombie = Instantiate(zombiePrefab, spawnPoint.position, spawnPoint.rotation);

        zombie.Setup(moveSpeed, runSpeed);
    }
}
