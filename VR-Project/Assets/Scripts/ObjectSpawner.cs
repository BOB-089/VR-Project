using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private XRSocketInteractor socket;
    [SerializeField] private GameObject destroyParticles;
    [SerializeField] private float respawnTime = 1f;
    [SerializeField] private float respawnDistance = 10f;
    [SerializeField] private int maxInstances = 5;

    private List<GameObject> currentInstances = new();
    private GameObject hiddenInstance;
    private float respawnTimer;
  
    void Start()
    {
        SpawnInstance();
    }

    void Update()
    {
        currentInstances.RemoveAll(instance => instance == null);
        ValidateSocketedInstance();
        DestroyDistantInstances();
        HandleRespawnTimer();
    }

    void OnEnable()
    {
        if (hiddenInstance != null) 
        { 
            hiddenInstance.SetActive(true); 
            hiddenInstance = null; 
        }
    }

    public void DisableSocketedObject()
    {
        if (socket.hasSelection) 
        { 
            hiddenInstance = socket.firstInteractableSelected.transform.gameObject; 
            hiddenInstance.SetActive(false);
        }
    }

    private void SpawnInstance()
    {
        if (!socket.hasSelection && currentInstances.Count < maxInstances)
        {
            currentInstances.Add(Instantiate(prefab, socket.transform.position, socket.transform.rotation));
            respawnTimer = respawnTime;
        }
    }

    private void DestroyInstance(GameObject instance)
    {
        Instantiate(destroyParticles, instance.transform.position, instance.transform.rotation);
        Destroy(instance);
    }

    private void DestroyDistantInstances()
    {
        foreach (GameObject instance in currentInstances)
        {
            float distance = Vector3.Distance(instance.transform.position, socket.transform.position);
            if (distance > respawnDistance) { DestroyInstance(instance); }
        }
    }

    private void ValidateSocketedInstance()
    {
        if (socket.hasSelection && !currentInstances.Contains(socket.firstInteractableSelected.transform.gameObject))
        {
            DestroyInstance(socket.firstInteractableSelected.transform.gameObject);
        }
    }

    private void HandleRespawnTimer()
    {
        if (socket.hasSelection) { return; }
        respawnTimer -= Time.deltaTime;
        if (respawnTimer <= 0f) { SpawnInstance(); }
    }
}