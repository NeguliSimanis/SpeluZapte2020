using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Checks if player is located on ground or flying/jumping
/// </summary>
public class GroundCheck : MonoBehaviour
{
    [SerializeField]
    PlayerController playerController;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            playerController.isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            playerController.isGrounded = false;
        }
    }
}
