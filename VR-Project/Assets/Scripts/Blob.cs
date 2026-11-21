using UnityEngine;
using UnityEngine.AI;

public class Blob : Enemy
{
    [SerializeField] private float detectionRadius = 5f;
    [SerializeField] private float wanderRadius = 3f;
    [SerializeField] private float attackRadius = 0.1f;
    private NavMeshAgent agent;
    private Hero targetHero;

    protected void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(GetRandomPosition());
    }

    private void Update()
    {
        Move();
        if (targetHero != null && Vector3.Distance(transform.position, targetHero.transform.position) <= attackRadius) 
        { 
            // TODO attack speed logic
            Attack(); 
        }
    }

    public override void Attack()
    {
        // TODO attack logic
    }

    public override void Move()
    {
        targetHero = HeroesManager.Instance.GetClosestHero(transform.position);

        if (targetHero != null && Vector3.Distance(transform.position, targetHero.transform.position) <= detectionRadius)
        {
            agent.SetDestination(targetHero.transform.position);
        }
        else if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            agent.SetDestination(GetRandomPosition());
        }
    }

    private Vector3 GetRandomPosition()
    {
        Vector2 randomPoint = Random.insideUnitCircle * wanderRadius;
        return transform.position + new Vector3( randomPoint.x, 0f, randomPoint.y);
    }
}
