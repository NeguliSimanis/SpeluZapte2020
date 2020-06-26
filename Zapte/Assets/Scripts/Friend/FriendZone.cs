using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// outer friend zone - player can interact with friend
/// inner friend zone - player takes damage
/// </summary>
public class FriendZone : MonoBehaviour
{
    [SerializeField]
    bool isInnerFriendZone;

    FriendController friendController;

    private void Start()
    {
        friendController = transform.parent.gameObject.GetComponent<FriendController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!isInnerFriendZone)
            {
                Debug.Log("should activate friend zone");
                friendController.ActivateFriendZone(true);
            }
            else
                friendController.HurtPlayer();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !isInnerFriendZone)
        {
            friendController.ActivateFriendZone(false);
        }
    }
}
