using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendController : MonoBehaviour
{
    [SerializeField]
    Animator friendAnimator;

    bool playerDetected = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !playerDetected)
        {
            playerDetected = true;
            ProcessPlayerCollision();
        }
    }

    private void ProcessPlayerCollision()
    {
        Debug.Log("yay");
        Destroy(gameObject);
    }
}
