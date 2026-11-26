using UnityEngine;

public class DartGoblin : Enemy
{
    [Header("Dart")]
    [SerializeField] private BasicProjectile projectile;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float retreatDistance = 0.5f;

    protected override void Attack()
    {
        BasicProjectile currentProjectile = Instantiate(projectile, firePoint.position, Quaternion.identity);
        currentProjectile.Initialise(this, target, damage);
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

    protected override void UpdateLookDirection()
    {
        if (target == null) { base.UpdateLookDirection(); return; }
        Vector3 direction = target.transform.position - transform.position;
        direction.y = 0f;
        transform.rotation = Quaternion.LookRotation(direction.normalized);
    }
}
