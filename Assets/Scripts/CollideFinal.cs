using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportPlayer : CollidableObject
{
    public GameObject playerObject;
    public Transform finalposition;

    private void OnCollided(Collider other)
    {
        if (other.gameObject == playerObject && other.CompareTag("Player"))
        {
            TransportPlayerToTarget();
        }
    }

    private void TransportPlayerToTarget()
    {
        if (finalposition != null)
        {
            playerObject.transform.position = finalposition.position;
        }
        else
        {
            Debug.LogWarning("Target object not assigned!");
        }
    }
}
