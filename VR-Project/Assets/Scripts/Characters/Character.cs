using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public abstract class Character : MonoBehaviour
{
    [Header("Combat")]
    [SerializeField] protected int health = 10;
    [SerializeField] protected int damage = 5;
    [SerializeField] protected float attackRadius = 0.2f;
    [SerializeField] protected float attackCooldown = 1f;
    protected float attackTimer = 0;

    [Header("AI")]
    [SerializeField] protected float detectionRadius = 0.5f;
    [SerializeField] protected float wanderRadius = 1f;
    private XRGrabInteractable grabInteractable;
    protected NavMeshAgent agent;
    protected Character target;
    protected float distanceToTarget;

    private bool IsActivated()
    {
        if (!agent.enabled)
        {
            if (!grabInteractable.isSelected && NavMesh.SamplePosition( transform.position, out _, 0.15f, NavMesh.AllAreas))
            {
                agent.enabled = true;
                agent.SetDestination(GetRandomPosition());
            }
        }
        if (grabInteractable.isSelected) { agent.enabled = false; return false; }
        return agent.isOnNavMesh;
    }

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (!IsActivated()) { return; }
        attackTimer -= Time.deltaTime;
        UpdateBehaviour();
        UpdateLookDirection();
    }

    private void UpdateBehaviour()
    {
        if ((target = GetTarget()) != null)
        {
            distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
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

    protected virtual void UpdateLookDirection()
    {
        if (agent.velocity.sqrMagnitude > 0.0001f) { transform.rotation = Quaternion.LookRotation(agent.velocity.normalized); }
    }
    protected virtual void Die() { Destroy(gameObject); }
    protected abstract Character GetTarget();
    protected abstract void Attack();
    protected abstract void Move();
    protected abstract void OnDestroy();
}