using UnityEngine;

public abstract class Hero : MonoBehaviour
{
    [SerializeField] protected int health = 100;
    [SerializeField] protected int damage = 5;

    protected void Start()
    {
        HeroesManager.Instance.AddHero(this);
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
        Destroy(gameObject);
    }

    protected void OnDestroy()
    {
        HeroesManager.Instance.RemoveHero(this);
    }
}