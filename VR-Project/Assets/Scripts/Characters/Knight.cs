public class Knight : Hero
{
    protected override void Attack()
    {
        target.TakeDamage(damage);
    }

    protected override void Move()
    {
        agent.SetDestination(target.transform.position);
    }
}
