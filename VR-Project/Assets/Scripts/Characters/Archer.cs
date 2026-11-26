using UnityEngine;

public class Archer : Hero
{
    [SerializeField] private float retreatDistance = 0.5f;

    protected override void Attack()
    {
        target.TakeDamage(damage);
    }

    protected override void Move()
    {
        if (distanceToTarget < retreatDistance) 
        { 
            Vector3 directionAway = (transform.position - target.transform.position).normalized;
            agent.SetDestination(transform.position + directionAway * retreatDistance);
        }
        else if (distanceToTarget > attackRadius)
        {
            agent.SetDestination(target.transform.position);
        }
        else
        {
            agent.ResetPath();
        }
    }
}