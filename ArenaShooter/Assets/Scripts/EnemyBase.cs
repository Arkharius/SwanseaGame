using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour, IDamageable {

    [SerializeField]
    private Rigidbody2D m_rigidbody;

    [SerializeField]
    [Range(0.1f, 2.0f)]
    private float m_turnRate = 0.5f;

    [SerializeField]
    private float m_movementSpeed;

    [SerializeField]
    private int m_hitPoints = 5;
    

    public void Die()
    {

    }

    public void TakeDamage(int damage)
    {
        m_hitPoints -= damage;

        if (m_hitPoints <= 0)
            Die();
    }

    protected void HandleMovement(Vector3 destination)
    {
        // track towards player
        Vector2 targetVector = destination - transform.position;
        float targetAngle = Vector2.SignedAngle(transform.up, targetVector);

        // Turn towards target
        m_rigidbody.AddTorque(targetAngle * m_turnRate);

        // move towards target     
        m_rigidbody.AddForce(transform.up * m_movementSpeed);
    }
}
