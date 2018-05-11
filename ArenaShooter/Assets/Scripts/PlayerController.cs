using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable {

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

    [SerializeField]
    private float m_shotsPerSecond;
    private float m_shotCoolDown;

    [SerializeField]
    private PlayerBulletPooler m_playerBulletPooler;

    [SerializeField]
    private float m_playerBulletSpeed;

    private bool m_boosterEnabled = false;
    [SerializeField]
    private float m_boosterMultiplier;
    [SerializeField]
    private ParticleSystem m_boosterParticles;

    [SerializeField]
    private int m_hitpoints;
    private int m_maxHitPoints;

    [SerializeField]
    private float m_powerShotSpeedMultiplier;

    private bool m_powerShot = false;

    [SerializeField]
    private ParticleSystem m_trailParticles;

    [SerializeField]
    private ParticleSystem m_ReverseTrailParticles;

    // set bullet origin points -- exposed as public to display in the Unity editor.
    public Vector2 m_bulletOriginLeft = Vector2.zero;
    public Vector2 m_bulletOriginRight = Vector2.zero;

    // draw editor controls for moving bullet origin points.
    public bool drawGizmos = false;
    public void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.TransformPoint(m_bulletOriginLeft), 0.1f);
        Gizmos.DrawWireSphere(transform.TransformPoint(m_bulletOriginRight), 0.1f);
    }

    // Use this for initialization
    void Start() {
        // set rotation drag speed
        m_rigidBody.angularDrag = m_rotationDrag;
        m_maxHitPoints = m_hitpoints;
    }

    // Update is called once per frame
    void FixedUpdate() {
        HandleMovement();
        HandleFiring();
        HandlePowerShot();
        HandleParticles();
    }

    private void HandleParticles() {
        if (Input.GetAxis("Vertical") > 0 && m_trailParticles != null) {
            m_trailParticles.Play();
        }
        else if (m_trailParticles != null) {
            m_trailParticles.Stop();
        }

        if (Input.GetAxis("Vertical") < 0 && m_ReverseTrailParticles != null) {
            m_ReverseTrailParticles.Play();
        }
        else if (m_ReverseTrailParticles != null) {
            m_ReverseTrailParticles.Stop();
        }
    }

    private void HandlePowerShot() {
        // if powershot is enabled make it available for firing
        if (m_powerShot && Input.GetKey(KeyCode.LeftAlt)) {
            m_powerShot = false;

            GameObject newPowerShot = m_playerBulletPooler.GetPowerShot();
            newPowerShot.transform.position = transform.position;
            newPowerShot.transform.rotation = transform.rotation;

            newPowerShot.GetComponent<Rigidbody2D>().velocity = newPowerShot.transform.up * m_powerShotSpeedMultiplier;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        // pickups ----------------------------------
        // power shot pickup ------------------------
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pickups") && collision.gameObject.tag == "PowerShot") {

            // check to see if we need this pickup
            if (!m_powerShot) {
                // set the shot back on
                m_powerShot = true;
                // remove the pickup
                Destroy(collision.gameObject);
            }
        }

        // hitpoints pickup -------------------------
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pickups") && collision.gameObject.tag == "HitPoints") {

            // check to see if we need any hitpoints, if not we can leave the pickup where it is.
            if (m_hitpoints < m_maxHitPoints) {
                // add the hitpoints back in
                m_hitpoints += collision.gameObject.GetComponent<HitPointPickupScript>().Hitpoints;
                if (m_hitpoints > m_maxHitPoints) m_hitpoints = m_maxHitPoints;
                // remove the pickup
                Destroy(collision.gameObject);
            }
        }
    }

    private void HandleFiring() {
        // start cooldown for shots
        if (m_shotCoolDown > 0f) {
            m_shotCoolDown -= Time.deltaTime;
        }
        if (m_shotCoolDown <= 0f && Input.GetKey(KeyCode.Space)) {

            //shot cooldown
            m_shotCoolDown = 1f / m_shotsPerSecond;

            // left bullet spawn - get a new bullet from the pooler
            GameObject newBulletLeft = m_playerBulletPooler.GetPlayerBullet();

            // set bullet position and rotation using ship position and offsets in bulletOriginLeft
            newBulletLeft.transform.position = transform.TransformPoint(m_bulletOriginLeft);
            newBulletLeft.transform.rotation = transform.rotation;
            newBulletLeft.GetComponent<Rigidbody2D>().velocity = newBulletLeft.transform.up * m_playerBulletSpeed;

            // right bullet spawn - get a new bullet from the pooler
            GameObject newBulletRight = m_playerBulletPooler.GetPlayerBullet();

            // set bullet position and rotation using ship position and offsets in bulletOriginRight 
            newBulletRight.transform.position = transform.TransformPoint(m_bulletOriginRight);// transform.position + new Vector3(bulletOriginRight.x, bulletOriginRight.y);
            newBulletRight.transform.rotation = transform.rotation;
            newBulletRight.GetComponent<Rigidbody2D>().velocity = newBulletRight.transform.up * m_playerBulletSpeed;
        }
    }

    private void HandleMovement() {
        // grab axis
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");
        bool boosterPressed = (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));

        if (m_boosterEnabled)
        {
            if(!boosterPressed)
            {
                m_boosterEnabled = false;
                m_maxThrustSpeed /= m_boosterMultiplier;
                m_boosterParticles.Stop();
            }
            verticalMovement *= m_boosterMultiplier;
            horizontalMovement /= m_boosterMultiplier;
        } else
        {
            if (boosterPressed)
            {
                m_boosterEnabled = true;
                m_maxThrustSpeed *= m_boosterMultiplier;
                m_boosterParticles.Play();
            }
        }

        

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

        // grab a 0-1 range float for % of currentspeed (m_rigidBody.velocity.magnitude) / top speed
        float magnitude = Mathf.Abs(m_rigidBody.velocity.magnitude) / m_maxThrustSpeed;
        // Scale addtional thrust relative to distance from top speed
        magnitude = 1f - Mathf.Pow(magnitude, 3);

        // add relative force (vertical axis) input * distance from top speed (0-1)
        m_rigidBody.AddRelativeForce(new Vector2(0f, additionalThrust * magnitude));

        //Debug.Log("VT=" + (Mathf.Abs(additionalThrust) + Mathf.Abs(m_rigidBody.velocity.y)) + ": MRS=" + m_maxThrustSpeed);
        //Debug.Log("RB.V=" + Mathf.Abs(m_rigidBody.velocity.y));
        //Debug.Log("AT=" + additionalThrust);
        //Debug.Log("velocity.y=" + Mathf.Abs(m_rigidBody.velocity.magnitude));

        //m_rigidBody.AddForce(new Vector2(horizontalMovement * m_horizontalMovementSpeed, verticalMovement * m_verticalMovementSpeed));
    }

    // interface methods from IDamageable
    public void Die() {
        Debug.Log("Dead");
    }
    public void TakeDamage(int damage) {
        m_hitpoints -= damage;
        if (m_hitpoints <= 0) Die();
    }
}
