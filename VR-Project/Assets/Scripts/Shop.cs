using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private WaveSpawner waveSpawner;
    [SerializeField] private GameObject blobPrefab;
    [SerializeField] private GameObject blobSpawningLvl2Button;
    [SerializeField] private GameObject blobSpawningLvl3Button;
    [SerializeField] private GameObject archerSpawner;
    [SerializeField] private GameObject archerSpawnerButton;

    public void BuyArcherSpawner()
    {
        if (MoneyManager.Instance.SpendMoney(20)) 
        { 
            archerSpawner.SetActive(true); 
            archerSpawnerButton.SetActive(false);
        }
    }

    public void BuyBlobSpawningLvl1()  { waveSpawner.AddEntry(blobPrefab, 3, 10f, 0.5f, 0.1f); }

    public void BuyBlobSpawningLvl2() 
    {
        if (MoneyManager.Instance.SpendMoney(10))
        {
            waveSpawner.IncreaseSpawnAmount(blobPrefab, 2);
            blobSpawningLvl2Button.SetActive(false);
            blobSpawningLvl3Button.SetActive(true);
        }
    }

    public void BuyBlobSpawningLvl3()
    {
        if (MoneyManager.Instance.SpendMoney(50))
        {
            waveSpawner.IncreaseSpawnAmount(blobPrefab, 5);
            blobSpawningLvl3Button.SetActive(false);
        }
    }
}