using UnityEngine;
using UnityEngine.AI;

public abstract class Character : MonoBehaviour
{
    [Header("Combat")]
    [SerializeField] protected int health = 10;
    [SerializeField] protected int damage = 5;
    [SerializeField] protected float attackRadius = 0.1f;
    [SerializeField] protected float attackCooldown = 1f;
    protected float attackTimer = 0;

    [Header("Detection")]
    [SerializeField] protected float detectionRadius = 5f;
    [SerializeField] protected float wanderRadius = 3f;
    protected NavMeshAgent agent;
    protected Character target;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(GetRandomPosition());
    }

    private void Update()
    {
        attackTimer -= Time.deltaTime;
        UpdateBehaviour();
    }

    private void UpdateBehaviour()
    {
        if ((target = GetTarget()) != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
            if (distanceToTarget <= detectionRadius) { HandleTargetDetected(distanceToTarget); return; }
        }
        Wander();
    }

    private void HandleTargetDetected(float distanceToTarget)
    {
        Move();
        if (distanceToTarget <= attackRadius && attackTimer <= 0f)
        {
            Attack();
            attackTimer = attackCooldown;
        }
    }

    private void Wander()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            agent.SetDestination(GetRandomPosition());
        }
    }

    private Vector3 GetRandomPosition()
    {
        Vector2 randomPoint = Random.insideUnitCircle * wanderRadius;
        return transform.position + new Vector3(randomPoint.x, 0f, randomPoint.y);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0) { Die(); }
    }

    protected virtual void Die() { Destroy(gameObject); }
    protected abstract Character GetTarget();
    protected abstract void Attack();
    protected abstract void Move();
    protected abstract void OnDestroy();
}