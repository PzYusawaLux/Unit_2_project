using UnityEngine;

public class PlayerSizeController : MonoBehaviour
{
    public float growthRate = 0.05f;
    public float maxSize = 5.0f;
    public float growthInterval = 1.0f;

    private bool hasCollided = false;
    private float elapsedTime = 0f;

    private void Update()
    {
        if (hasCollided)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= growthInterval)
            {
                GrowPlayer(gameObject);
                elapsedTime = 0f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasCollided && other.CompareTag("Player"))
        {
            hasCollided = true;
        }
    }

    private void GrowPlayer(GameObject playerObject)
    {
        Vector3 currentScale = playerObject.transform.localScale;
        float newSize = currentScale.x + growthRate;

        // Limit the size of the player
        if (newSize <= maxSize)
        {
            playerObject.transform.localScale = new Vector3(newSize, newSize, 1f);
        }
    }
}
