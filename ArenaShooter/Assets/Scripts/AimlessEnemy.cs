using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimlessEnemy : EnemyBase {

    private Vector3 destination;

    private float newDestinationDelay = 5f;


	// Use this for initialization
	void Start () {

        destination = transform.up * 2;
    }

    // Update is called once per frame
    void Update ()
    {
        HandleMovement(destination);

        // New destination if close enough to current destination
        Vector3 distance = transform.position - destination;
        if (distance.magnitude < 0.1f) FindNewDestination();

        // New destination if current destination expired
        newDestinationDelay -= Time.deltaTime;
        if (newDestinationDelay <= 0) FindNewDestination();
    }

    private void FindNewDestination()
    {
        Vector2 randomCircle = Random.insideUnitCircle * 2;
        destination = transform.position + new Vector3(randomCircle.x, randomCircle.y, 0);
        newDestinationDelay = Random.Range(1.0f, 5.0f);
    }
}
