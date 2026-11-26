using System.Collections.Generic;
using UnityEngine;

public class HeroesManager : MonoBehaviour
{
    private List<Hero> heroes = new();

    public static HeroesManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void AddHero(Hero hero)
    {
        heroes.Add(hero);
    }

    public void RemoveHero(Hero hero)
    {
        heroes.Remove(hero);
    }

    public Character GetClosestHero(Vector3 position)
    {
        Character closestHero = null;
        float closestDistance = Mathf.Infinity;
        foreach (Hero hero in heroes)
        {
            float distance = Vector3.Distance(position, hero.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestHero = hero;
            }
        }
        return closestHero;
    }
}