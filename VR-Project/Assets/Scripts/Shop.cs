using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private WaveSpawner waveSpawner;
    [SerializeField] private GameObject blobPrefab;

    public void BuyBlobSpawningLvl1()  { waveSpawner.AddEntry(blobPrefab, 3, 10f, 0.5f, 0.1f); }

    public void BuyBlobSpawningLvl2() 
    {
        if (MoneyManager.Instance.SpendMoney(10))
        {
            waveSpawner.IncreaseSpawnAmount(blobPrefab, 2);
        }
    }

    public void BuyBlobSpawningLvl3()
    {
        if (MoneyManager.Instance.SpendMoney(50))
        {
            waveSpawner.IncreaseSpawnAmount(blobPrefab, 5);
        }
    }
}