using UnityEngine;
using System.Collections;

public class CollidingTrigger : CollidableObject
{
    public GameObject playerObject;
    public GameObject targetObject;
    public GameObject movementObject;

    private Vector3 originalPlayerScale;
    private bool interactionTriggered = false;
    private float speed = 6f;
    private float disappearPoint = 70f;

    protected override void Start()
    {
        base.Start();
        originalPlayerScale = playerObject.transform.localScale;
    }

    protected override void OnCollided(GameObject collidedObject)
    {
        if (!interactionTriggered && collidedObject == playerObject)
        {
            ChangePlayerSize();
            ActivateTargetObject();
            MoveMovementObject();
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
            Debug.LogWarning("Target object not assigned!");
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
            Debug.LogWarning("Movement object not assigned!");
        }
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

}
