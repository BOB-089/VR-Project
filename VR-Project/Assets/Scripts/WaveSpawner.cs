using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private List<SpawnEntry> spawnEntries = new();

    [System.Serializable] public class SpawnEntry
    {
        public GameObject prefab;
        public int amount = 1;
        public float spawnInterval = 5f;
        public float spawnHeight = 0f;
        public float clearanceRadius = 0.1f;
        [HideInInspector] public float timer;
    }

    [SerializeField] private float spawnRadius = 1f;

    private void Start()
    {
        foreach (SpawnEntry spawnEntry in spawnEntries)
        {
            spawnEntry.timer = spawnEntry.spawnInterval;
        }
    }

    void Update()
    {
        foreach (SpawnEntry spawnEntry in spawnEntries)
        {
            spawnEntry.timer -= Time.deltaTime;

            if (spawnEntry.timer <= 0f)
            {
                for (int i = 0; i < spawnEntry.amount; i++)
                {
                    Spawn(spawnEntry);
                }

                spawnEntry.timer = spawnEntry.spawnInterval;
            }
        }
    }

    private void Spawn(SpawnEntry spawnEntry)
    {
        Vector3 spawnPosition;
        int attempts = 0;
        float height = spawnEntry.spawnHeight;
        do
        {
            Vector2 randomPoint = Random.insideUnitCircle * spawnRadius;
            spawnPosition = transform.position + new Vector3(randomPoint.x, height, randomPoint.y);
            attempts++;
            if (attempts > 10) 
            {
                height += 0.1f;
                attempts = 0; 
            }
        }
        while (Physics.CheckSphere(spawnPosition, spawnEntry.clearanceRadius));

        Instantiate(spawnEntry.prefab, spawnPosition, Quaternion.identity);
    }
}