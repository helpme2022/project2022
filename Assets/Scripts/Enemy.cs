using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHp;

    void Start()
    {
        currentHp = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHp -= damage;
    }

    void Die()
    {
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}
