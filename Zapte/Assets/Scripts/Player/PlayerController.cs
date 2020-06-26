using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D playerRigidbody2D;

    #region movement data
    [HideInInspector]
    public bool isGrounded;
    [HideInInspector]
    public bool allowJump = false;
    [HideInInspector]
    public bool allowWalk = true;
    float jumpForce = 500f;
    float moveForce = 200f;
    #endregion

    #region animation data
    [SerializeField]
    Animator playerAnimator;
    #endregion

    void Start()
    {
        isGrounded = false;
        allowJump = false;
        allowWalk = false;
        playerRigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }

    public void StartGame()
    {
        allowJump = true;
        allowWalk = true;
    }

    void Update()
    {
        CheckPlayerInput();
    }

    private void LateUpdate()
    {
        // update animations
        playerAnimator.SetBool("isGrounded", isGrounded);
    }

    private void CheckPlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && allowJump)
        {
            playerRigidbody2D.AddForce(Vector2.up * jumpForce);
            isGrounded = false;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && allowWalk)
        {
            playerRigidbody2D.AddForce(Vector2.right * moveForce);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && allowWalk)
        {
            playerRigidbody2D.AddForce(Vector2.left * moveForce);
        }
    }
}
