using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private List<SpawnEntry> spawnEntries = new();


    [System.Serializable] public class SpawnEntry
    {
        public GameObject prefab;
        public int amount;
        public float spawnInterval;
        public float spawnHeight;
    }

    [SerializeField] private float spawnRadius = 1f;

    void Update()
    {
        
    }

    private void SpawnWave()
    {
        float spawnDistance = Random.Range(0f, spawnRadius);
        float spawnAngle = Random.Range(0f, 360f);
        //Vector3 spawnPosition = transform.position + new Vector3(x, 0f, z);
    }
}
