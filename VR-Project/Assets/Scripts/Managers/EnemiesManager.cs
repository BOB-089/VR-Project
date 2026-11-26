using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] private TMP_Text enemiesCounter;
    [SerializeField] private Image enemiesBar;
    [SerializeField] private int maxEnemies;
    private List<Enemy> enemies = new();

    public static EnemiesManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void AddEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
        UpdateEnemyCount();
    }

    public void RemoveEnemy(Enemy enemy)
    {
        enemies.Remove(enemy);
        UpdateEnemyCount();
    }

    private void UpdateEnemyCount()
    {
        if (enemiesCounter == null || enemiesBar == null) { return; }
        enemiesCounter.text = $"Enemies {enemies.Count}/{maxEnemies}";
        enemiesBar.fillAmount = (float)enemies.Count / maxEnemies;
        if (enemies.Count >= maxEnemies) { GameManager.Instance.EndGame(); }
    }

    public Character GetClosestEnemy(Vector3 position)
    {
        Character closestEnemy = null;
        float closestDistance = Mathf.Infinity;
        foreach (Enemy enemy in enemies)
        {
            float distance = Vector3.Distance(position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }
        return closestEnemy;
    }
}