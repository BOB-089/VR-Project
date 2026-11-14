using UnityEngine;

public class ObjectWithSpawners : MonoBehaviour
{
    public void Disable()
    {
        foreach (ObjectSpawner objectSpawner in GetComponentsInChildren<ObjectSpawner>())
        {
            objectSpawner.DisableSocketedObject();
        }
        this.gameObject.SetActive(false);
    }
}