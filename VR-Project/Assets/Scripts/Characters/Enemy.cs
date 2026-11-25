using UnityEngine;

public abstract class Enemy : Character
{
    [SerializeField] protected int moneyDropped = 1;

    private void Start() { EnemiesManager.Instance.AddEnemy(this); }

    protected override Character GetTarget()
    {
        return HeroesManager.Instance.GetClosestHero(transform.position);
    }

    protected override void Die()
    {
        MoneyManager.Instance.AddMoney(moneyDropped);
        base.Die();
    }

    protected override void OnDestroy() { EnemiesManager.Instance?.RemoveEnemy(this); }
}