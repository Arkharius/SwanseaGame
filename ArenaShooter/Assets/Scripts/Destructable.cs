using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour, IDamageable {

    [SerializeField]
    private int m_hitPoints = 10;

	public void TakeDamage(int damage)
    {
        m_hitPoints -= damage;

        if (m_hitPoints <= 0)
            Die();
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
