using UnityEngine;

public abstract class Hero : Character
{
    private void Start() { HeroesManager.Instance.AddHero(this); }

    protected override Character GetTarget()
    {
        return EnemiesManager.Instance.GetClosestEnemy(transform.position);
    }

    protected override void OnDestroy() { HeroesManager.Instance?.RemoveHero(this); }
}