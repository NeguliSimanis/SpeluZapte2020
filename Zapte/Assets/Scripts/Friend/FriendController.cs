using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FriendTextGenerator
{
    string alphabet = "abcdefghijklmnopqrstuvwxyz";

    public string GetFriendText()
    {
        string friendText = "default";
        friendText = "" + alphabet[Random.Range(0,alphabet.Length)];
        return friendText;
    }
}

public class FriendController : MonoBehaviour
{
    [SerializeField]
    Animator friendAnimator;

    private bool isPlayerInFriendZone = false;

    #region friend text data
    [Header("TEXT DATA")]
    [SerializeField]
    private Text friendTextField;
    private Animator friendTextAnimator;
    private FriendTextGenerator friendTextGenerator;
    [HideInInspector]
    public string friendText;
    #endregion

    private void Start()
    {
        GetComponents();
        InitializeFriendText();
    }

    private void GetComponents()
    {
        friendTextAnimator = friendTextField.gameObject.GetComponent<Animator>();
    }

    private void InitializeFriendText()
    {
        friendTextGenerator = new FriendTextGenerator();
        friendText = friendTextGenerator.GetFriendText();
        friendTextField.text = friendText;
    }


    public void ActivateFriendZone(bool activate)
    {
        if (activate)
        {
            Debug.Log("player has entered friend zone");
            AnimateFriendText();
            isPlayerInFriendZone = true;
        }
        else
        {
            isPlayerInFriendZone = false;
            Debug.Log("player has exited friend zone");
        }
        
    }

    private void Update()
    {
        CheckPlayerInput();
    }

    private void CheckPlayerInput()
    {
        if (isPlayerInFriendZone) 
        {
            if (Input.inputString == friendText)
            {
                Debug.Log("RIGHT INPUT");
                DestroySelf();
            }
            else if (Input.inputString != null)
            {
                Debug.Log("WRONG INPUT");
               // HurtPlayer();
            }
        }
    }

    private void AnimateFriendText()
    {
        friendTextAnimator.SetBool("highlight", true);
    }

    public void HurtPlayer()
    {
        Debug.Log("MUST HURT PLAYER");
        DestroySelf();;
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
