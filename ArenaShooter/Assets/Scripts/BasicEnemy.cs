using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : EnemyBase {

    private Transform m_target;


    // Use this for initialization
    void Start () {
        m_target = GameObject.FindGameObjectWithTag("Player").transform;
	}


    // Update is called once per frame
    void FixedUpdate ()
    {
        HandleMovement(m_target.position);
    }

}
