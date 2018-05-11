using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable {

    private Transform m_target;
    private Rigidbody2D m_rigidbody;

    [SerializeField]
    [Range(0.1f, 2.0f)]
    private float m_turnRate = 0.5f;
        
    [SerializeField]
    private float m_movementSpeed;

    [SerializeField]
    private int m_hitPoints = 5;

    // Use this for initialization
    void Start () {
        m_target = GameObject.FindGameObjectWithTag("Player").transform;
        m_rigidbody = GetComponent<Rigidbody2D>();
	}


    // Update is called once per frame
    void Update () {
        // track towards player
        Vector2 targetVector =  m_target.position - transform.position;
        float targetAngle = Vector2.SignedAngle(transform.up, targetVector);

        //Debug.Log(targetAngle);
        m_rigidbody.AddTorque(targetAngle*m_turnRate);

        // move towards player if within threshold        
        m_rigidbody.AddForce(transform.up * m_movementSpeed);
    }


    public void Die()
    {

    }

    public void TakeDamage(int damage)
    {
        m_hitPoints -= damage;

        if (m_hitPoints <= 0)
            Die();
    }    
}
