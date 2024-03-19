using UnityEngine;

public class RabitMovement : MonoBehaviour
{
    public float speed = 5f;
    public float disappearPoint = -74f;

    private void Start()
    {
        transform.position = new Vector3(-95f, -197.31f, 0f);
    }

    private void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        if (transform.position.x >= disappearPoint)
        {
            gameObject.SetActive(false);
        }
    }
}
