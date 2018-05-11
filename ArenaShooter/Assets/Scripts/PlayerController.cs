using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private Rigidbody2D m_rigidBody;

    [SerializeField]
    private float m_moveMultiplier;

    [SerializeField]
    private float m_turnMultiplier;

    [SerializeField]
    private float m_maxRotationSpeed;

    [SerializeField]
    private float m_maxThrustSpeed;

    [SerializeField]
    private float m_rotationDrag;

    // Use this for initialization
    void Start () {
        // set rotation drag speed
        m_rigidBody.angularDrag = m_rotationDrag;
    }
	
	// Update is called once per frame
	void Update () {
        HandleMovement();
	}

    private void HandleMovement() {
        // grab axis
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        // do rotation ---------------------------------------------------------------------
        // get current rotation speed
        float currentAngularVelocity = m_rigidBody.angularVelocity;

        // additional rotation speed
        float newAngularVelocity = (-horizontalMovement * m_turnMultiplier);

        // check if current rotation speed + additional rotation > max rotation speed
        if ((Mathf.Abs(newAngularVelocity) + Mathf.Abs(currentAngularVelocity)) > m_maxRotationSpeed) newAngularVelocity = 0f;

        // add the addition rotation speed
        m_rigidBody.AddTorque(newAngularVelocity);
        //Debug.Log("AV=" + (Mathf.Abs(newAngularVelocity) + Mathf.Abs(currentAngularVelocity)) + ": MRS=" + m_maxRotationSpeed);

        // add thrust ---------------------------------------------------------------------
        // calc additional thrust
        float additionalThrust = verticalMovement * m_moveMultiplier;

        // check that it does not exceed bounds
        if ((Mathf.Abs(additionalThrust) + Mathf.Abs(m_rigidBody.velocity.magnitude)) > m_maxThrustSpeed) additionalThrust = 0f;

        // add relative force (vertical axis)
        m_rigidBody.AddRelativeForce(new Vector2(0f, additionalThrust));

        //Debug.Log("VT=" + (Mathf.Abs(additionalThrust) + Mathf.Abs(m_rigidBody.velocity.y)) + ": MRS=" + m_maxThrustSpeed);
        //Debug.Log("RB.V=" + Mathf.Abs(m_rigidBody.velocity.y));
        //Debug.Log("AT=" + additionalThrust);
        //Debug.Log("velocity.y=" + Mathf.Abs(m_rigidBody.velocity.magnitude));

        //m_rigidBody.AddForce(new Vector2(horizontalMovement * m_horizontalMovementSpeed, verticalMovement * m_verticalMovementSpeed));



    }
}
