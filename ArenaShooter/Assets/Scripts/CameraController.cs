using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField]
    private Transform m_player;
    [SerializeField]
    [Range(0f, 3f)]
    private float m_movementSpeed;
    [SerializeField]
    [Range(0f, 10f)]
    private float m_leadAmount;
    private float myZ;

    private void Start()
    {
        myZ = transform.position.z;
    }
    // Update is called once per frame
    void Update () {
        Vector3 targetPosition = Vector3.Lerp(transform.position, m_player.position + (m_player.up * m_leadAmount), m_movementSpeed * Time.deltaTime);
        targetPosition.z = myZ;
        transform.position = targetPosition;
	}
}
