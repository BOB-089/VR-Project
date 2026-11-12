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

    void Start()
    {
        Spawn();
    }

    void Update()
    {
        currentInstances.RemoveAll(instance => instance == null);

        if (socket.hasSelection) 
        {
            if (!currentInstances.Contains(socket.firstInteractableSelected.transform.gameObject))
            {
                socket.EndManualInteraction();
            }
            return; 
        }

        respawnTimer -= Time.deltaTime;
        if (currentInstances.Count < maxInstances && respawnTimer <= 0f) { Spawn(); }

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
    }

    private void Spawn()
    {
        currentInstances.Add(Instantiate(prefab, socket.transform.position, socket.transform.rotation));
        respawnTimer = respawnTime;
    }
}
