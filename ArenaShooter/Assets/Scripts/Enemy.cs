using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable {

    private Transform m_target;
    private Rigidbody2D m_rigidbody;

    [SerializeField]
    private float m_turnRate, m_movementSpeed;

    private float m_angle;

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

        //bool turnRight = (Vector2.Dot(transform.right, targetVector) > 0);

        Debug.Log(targetAngle);
        m_rigidbody.AddTorque(targetAngle);

        // move towards player if within threshold        
        m_rigidbody.AddForce(transform.up * m_movementSpeed);
    }


    public void Die()
    {

    }

    public void TakeDamage(int damage)
    {

    }    
}
