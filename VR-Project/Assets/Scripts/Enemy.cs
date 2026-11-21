using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected int damage;
    [SerializeField] protected int moneyDropped;

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
        EnemiesManager.Instance.RemoveEnemy(this);
        Destroy(gameObject);
    }
}