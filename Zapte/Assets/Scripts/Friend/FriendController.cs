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

    AudioManager audioManager;

    private bool isPlayerInFriendZone = false;
    private bool hasHurtPlayer = false;
    private bool canHurtPlayer = true;

    #region friend text data
    [Header("TEXT DATA")]
    [SerializeField]
    private Text friendTextField;
    private Animator friendTextAnimator;
    private FriendTextGenerator friendTextGenerator;
    [HideInInspector]
    public string friendText;
    private UserInterfaceManager userInterfaceManager;
    #endregion

    PlayerController playerController;

    private void Start()
    {
        GetComponents();
        InitializeFriendText();
     
    }

    private void GetComponents()
    {
        friendTextAnimator = friendTextField.gameObject.GetComponent<Animator>();
        userInterfaceManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<UserInterfaceManager>();
        audioManager = GameManager.instance.audioManager;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void InitializeFriendText()
    {
        friendTextGenerator = new FriendTextGenerator();
        friendText = friendTextGenerator.GetFriendText();
        friendTextField.text = friendText.ToUpper();
    }


    public void ActivateFriendZone(bool activate)
    {
        if (activate)
        {
            //Debug.Log("player has entered friend zone");
            AnimateFriendText();
            isPlayerInFriendZone = true;
        }
        else
        {
            isPlayerInFriendZone = false;
            //Debug.Log("player has exited friend zone");
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
            if (Input.inputString.ToLower() == friendText)
            {
                AddRegularFriend();
                return;
            }
            else if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Space) && canHurtPlayer)
            {
                Debug.Log("input:  " + Input.inputString.ToLower() + ". Required: " + friendText);
                HurtPlayer();
                return;
            }
        }

        if (playerController.inFriendZone && PlayerStats.current.gainMultipleFriendsOnButtonPress)
        {
            Debug.Log("COULD BE FRIEND");
            if (PlayerStats.current.multipleFriendsGainEnabled && Random.Range(0f, 1f) < PlayerStats.current.gainMultipleFriendsChance)
            {
                AddAreaOfEffectFriend();
            }
            return;
        }
    }

    private void AnimateFriendText()
    {
        friendTextAnimator.SetBool("highlight", true);
    }

    public void HurtPlayer()
    {
        if (hasHurtPlayer)
            return;
        if (!canHurtPlayer)
            return;
        audioManager.PlayFriendLostSFX();
        hasHurtPlayer = true;
        //Debug.Log(gameObject.name + " MUST HURT PLAYER " + Time.time);
        userInterfaceManager.AddLife(-1);
        DestroySelf();
    }

    private void AddRegularFriend()
    {
        userInterfaceManager.AddFriend();
        audioManager.PlayFriendFoundSFX();
        StartCoroutine(DestroyGainedFriendAfterDelay());
       
    }

    private void AddAreaOfEffectFriend()
    {
        userInterfaceManager.AddFriend();
        PlayerStats.current.multipleFriendsGainEnabled = false;
        DestroySelf();
    }

    private IEnumerator DestroyGainedFriendAfterDelay()
    {
        if (PlayerStats.current.gainMultipleFriendsOnButtonPress)
            PlayerStats.current.multipleFriendsGainEnabled = true;
        canHurtPlayer = false;
        yield return new WaitForSeconds(0.2f);
        PlayerStats.current.multipleFriendsGainEnabled = false;
        DestroySelf();
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
