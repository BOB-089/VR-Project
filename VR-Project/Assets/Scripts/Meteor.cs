using UnityEngine;

public class Meteor : MonoBehaviour
{
    [SerializeField] private int damage = 100;
    [SerializeField] private float radius = 0.5f;

    private void OnCollisionEnter(Collision collision)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider collider in colliders)
        {
            Character character = collider.GetComponent<Character>();
            character?.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
