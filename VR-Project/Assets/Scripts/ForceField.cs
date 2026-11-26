using UnityEngine;

public class Forcefield : MonoBehaviour
{
    [SerializeField] private int health = 25;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0) { Destroy(gameObject); }
    }
}