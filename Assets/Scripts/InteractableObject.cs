using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : CollidableObject
{
    private bool interacted = false;

    public Transform playerTransform;
    public Transform doorTransform;
    public Collider2D doorCollider;
    public Collider2D cardGuard;
    public Collider2D rabitHoleGround;
    public GameObject rabitHoleGroundPic;

    public Vector3 growScale = new Vector3(0.7f, 0.7f, 0.7f);
    public Vector3 shrinkScale = new Vector3(0.2f, 0.2f, 1f);
    public Vector3 neutralizeScale = new Vector3(0.33f, 0.33f, 1f);
    public Vector3 drawScale = new Vector3(0.6f, 0.6f, 0.6f);

    public Transform rabbitHoleTarget;

    public GameObject conversationSprite;
    public bool activateOnCollision = true;

    public void ActivateConversationSprite()
    {
        if (conversationSprite != null)
        {
            //Debug.Log("conversation triggered");
            conversationSprite.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Conversation sprite not assigned!");
        }
    }

    private void RabbitHoleActivate()
    {
        if (rabitHoleGround != null)
        {
            rabitHoleGround.enabled = false;
        }
        else
        {
            Debug.LogWarning("Rabbit hole ground collider not assigned!");
        }

        if (rabitHoleGroundPic != null)
        {
            rabitHoleGroundPic.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Rabbit hole ground picture not assigned!");
        }
    }

    protected override void OnCollided(GameObject collidedObject)
    {
        if (Input.GetKeyDown(KeyCode.E) && !interacted && collidedObject.CompareTag("Player"))
        {
            OnInteract(collidedObject);
        }
    }

    //interaction
    protected virtual void OnInteract(GameObject collidedObject)
    {
        if (!interacted)
        {
            interacted = true;
            Debug.Log("Interacted with " + name);

            if (conversationSprite != null)
            {
                ActivateConversationSprite();
            }

            if (gameObject.CompareTag("RabbitHole"))
            {
                RabbitHoleActivate();
            }

            else if (gameObject.CompareTag("RedPotion"))
            {
                GrowPlayer();
            }
            else if (gameObject.CompareTag("BluePotion"))
            {
                ShrinkPlayer();
            }
            else if (gameObject.CompareTag("Key"))
            {
                OpenDoor();
            }

            else if (gameObject.CompareTag("PurpleMushroom"))
            {
                DrawPlayer();
            }
            else if (gameObject.CompareTag("PinkMushroom"))
            {
                NeutralizePlayer();
            }

            else if (gameObject.CompareTag("flowerWhite"))
            {
                Debug.Log("Destroying flowerWhite object");
                HideFlower();
            }


            interacted = false;
        }
    }

    //grow and shrink part
    private void GrowPlayer()
    {
        float xSign = Mathf.Sign(playerTransform.localScale.x);

        playerTransform.localScale = growScale;
        //added by purpose, or player flipped after using potion
        if (xSign < 0)
        {
            Vector3 newScale = playerTransform.localScale;
            newScale.x *= -1;
            playerTransform.localScale = newScale;
        }
    }

    private void ShrinkPlayer()
    {
        float xSign = Mathf.Sign(playerTransform.localScale.x);

        playerTransform.localScale = shrinkScale;

        if (xSign < 0)
        {
            Vector3 newScale = playerTransform.localScale;
            newScale.x *= -1;
            playerTransform.localScale = newScale;
        }
    }

    private void DrawPlayer()
    {
        float xSign = Mathf.Sign(playerTransform.localScale.x);

        playerTransform.localScale = drawScale;

        if (xSign < 0)
        {
            Vector3 newScale = playerTransform.localScale;
            newScale.x *= -1;
            playerTransform.localScale = newScale;
        }
    }

    private void NeutralizePlayer()
    {
        float xSign = Mathf.Sign(playerTransform.localScale.x);

        playerTransform.localScale = neutralizeScale;

        if (xSign < 0)
        {
            Vector3 newScale = playerTransform.localScale;
            newScale.x *= -1;
            playerTransform.localScale = newScale;
        }
    }

    private void OpenDoor()
    {
        if (doorCollider != null)
        {
            doorCollider.enabled = false;
        }

        //door opens, no animation
        doorTransform.Translate(Vector3.right * 0.9f);

        //hide key
        GameObject keyObject = GameObject.FindGameObjectWithTag("Key");
        if (keyObject != null)
        {
            keyObject.SetActive(false);
        }
    }

    private void HideFlower()
    {
        if (cardGuard != null)
        {
            cardGuard.enabled = false;
        }

        GameObject flowerWhite = GameObject.FindGameObjectWithTag("flowerWhite");
        if (flowerWhite != null)
        {
            flowerWhite.SetActive(false);
        }
    }
}
