using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D playerRigidbody2D;

    #region jumping data
    [HideInInspector]
    public bool isGrounded;
    float jumpForce = 200f;
    #endregion

    #region animation data
    [SerializeField]
    Animator playerAnimator;
    #endregion

    void Start()
    {
        isGrounded = false;
        playerRigidbody2D = gameObject.GetComponent<Rigidbody2D>();
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("JUMP PRESSED");
            playerRigidbody2D.AddForce(Vector2.up * jumpForce);
            isGrounded = false;
        }
    }
}
