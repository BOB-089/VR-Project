using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private XRSocketInteractor socket;
    [SerializeField] private float respawnTime = 1f;
    [SerializeField] private float respawnDistance = 10f;
    [SerializeField] private int maxInstances = 5;

    private List<GameObject> currentInstances = new();
    private float respawnTimer;

    private void OnEnable()
    {
        Spawn();
    }

    private void OnDisable() // doesnt work
    {
        if (socket.hasSelection)
        {
            Destroy(socket.firstInteractableSelected.transform.gameObject);
        }
    }

    void Update()
    {
        currentInstances.RemoveAll(instance => instance == null);

        if (socket.hasSelection) 
        {
            if (!currentInstances.Contains(socket.firstInteractableSelected.transform.gameObject))
            {
                Destroy(socket.firstInteractableSelected.transform.gameObject);
                // TODO: Particles
            }
            return; 
        }

        foreach (GameObject instance in currentInstances)
        {
            float distance = Vector3.Distance(instance.transform.position,  socket.transform.position);
            if (distance > respawnDistance)
            {
                instance.transform.SetPositionAndRotation(socket.transform.position, socket.transform.rotation);
                respawnTimer = respawnTime;
                break;
            }
        }

        respawnTimer -= Time.deltaTime;
        if (respawnTimer <= 0f) { Spawn(); }
    }

    private void Spawn()
    {
        if (!socket.hasSelection && currentInstances.Count < maxInstances)
        {
            currentInstances.Add(Instantiate(prefab, socket.transform.position, socket.transform.rotation));
            respawnTimer = respawnTime;
        }
    }
}
