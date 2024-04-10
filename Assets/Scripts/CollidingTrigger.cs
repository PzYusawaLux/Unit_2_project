using UnityEngine;
using System.Collections;

public class CollidingTrigger : CollidableObject
{
    public GameObject playerObject;
    public GameObject targetObject;
    public GameObject movementObject;
    public GameObject playerSleep;
    public Transform transportTarget;

    private Vector3 originalPlayerScale;
    private bool interactionTriggered = false;
    private float speed = 6f;
    private float disappearPoint = 70f;

    private Quaternion originalSleepRotation;

    protected override void Start()
    {
        base.Start();
        originalPlayerScale = playerObject.transform.localScale;
        originalSleepRotation = playerSleep.transform.rotation;
    }

    protected override void OnCollided(GameObject collidedObject)
    {
        if (!interactionTriggered && collidedObject == playerObject)
        {
            ChangePlayerSize();
            ActivateTargetObject();
            MoveMovementObject();
            TransportPlayer();

            RotatePlayerSleep();
            interactionTriggered = true;
        }
    }

    private void ChangePlayerSize()
    {
        playerObject.transform.localScale = new Vector3(-0.7f, 0.7f, 0.7f);
    }

    private void ActivateTargetObject()
    {
        if (targetObject != null)
        {
            targetObject.SetActive(true);
        }
        else
        {
            //Debug.LogWarning("Target object not assigned!");
        }
    }

    private void MoveMovementObject()
    {
        if (movementObject != null)
        {
            StartCoroutine(MoveObjectCoroutine());
        }
        else
        {
            //Debug.LogWarning("Movement object not assigned!");
        }
    }

    private void RotatePlayerSleep()
    {
        playerSleep.transform.Rotate(Vector3.forward, 120f);
        StartCoroutine(RotatePlayerSleepCoroutine());
    }

    private IEnumerator RotatePlayerSleepCoroutine()
    {
        float duration = 1.0f;
        float currentTime = 0.0f;
        Quaternion startRotation = playerSleep.transform.rotation;
        while (currentTime < duration)
        {
            float t = currentTime / duration;
            playerSleep.transform.rotation = Quaternion.Slerp(startRotation, originalSleepRotation, t);
            currentTime += Time.deltaTime;
            yield return null;
        }
        playerSleep.transform.rotation = originalSleepRotation;
    }

    private IEnumerator MoveObjectCoroutine()
    {
        Vector3 startPosition = new Vector3(105f, -195.5f, 0f);
        Vector3 endPosition = new Vector3(disappearPoint, -195.5f, 0f);

        float distance = Vector3.Distance(startPosition, endPosition);
        float startTime = Time.time;

        while (Time.time - startTime <= distance / speed)
        {
            float fraction = (Time.time - startTime) * speed / distance;
            movementObject.transform.position = Vector3.Lerp(startPosition, endPosition, fraction);
            yield return null;
        }

        movementObject.transform.position = endPosition;

        movementObject.SetActive(false);
    }

    private void TransportPlayer()
    {
        if (transportTarget != null)
        {
            playerObject.transform.position = transportTarget.position;
            playerObject.transform.localScale = originalPlayerScale;

        }
        else
        {
            //Debug.LogWarning("Transport target location not assigned!");
        }
    }

}
