using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

    [SerializeField]
    private float m_rotationSpeed = 15f;
	// Update is called once per frame
	void Update () {
        transform.Rotate(0f, 0f, m_rotationSpeed * Time.deltaTime);
	}
}
