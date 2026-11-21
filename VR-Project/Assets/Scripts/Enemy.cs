using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int health = 10;
    [SerializeField] protected int damage = 5;
    [SerializeField] protected int moneyDropped = 1;

    protected void Start()
    {
        EnemiesManager.Instance.AddEnemy(this);
    }

    public abstract void Attack();

    public abstract void Move();

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0) { Die(); }
    }

    protected void Die()
    {
        MoneyManager.Instance.AddMoney(moneyDropped);
        Destroy(gameObject);
    }

    protected void OnDestroy()
    {
        EnemiesManager.Instance.RemoveEnemy(this);
    }
}