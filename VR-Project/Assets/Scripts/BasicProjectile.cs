using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float lifetime = 5f;
    private int damage = 10;
    private Character owner;

    private void Start() { Destroy(gameObject, lifetime); }

    public void Initialise(Character owner, Character target, int damage)
    {
        this.owner = owner;
        this.damage = damage;
        transform.rotation = Quaternion.LookRotation((target.transform.position - transform.position).normalized);
    }

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        Character character = other.GetComponent<Character>();
        if (character == owner) { return; }
        character?.TakeDamage(damage);
        Destroy(gameObject);
    }
}