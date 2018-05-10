using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable {

    [SerializeField]
    private Transform m_target;

    [SerializeField]
    private float m_turnRate, m_movementSpeed;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Die()
    {

    }

    public void TakeDamage(int damage)
    {

    }    
}
