using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UserInterfaceManager : MonoBehaviour
{
    PlayerController playerController;
    string alphabet = "abcdefghijklmnopqrstuvwxyz";

    [SerializeField]
    Text friendCount;

    [SerializeField]
    Text lifeText;

    #region KEY INPUT HUD
    [Header("KEY INPUT HUD")]

    GameObject keyInputHUD;

    [SerializeField]
    Text keyInputText1;
    [SerializeField]
    Text keyInputText2;

    [SerializeField]
    AnimationClip keyInputAnimation;
    float keyInputAnimationTime;
    float keyInputAnimationEndTime;
    #endregion

    #region END GAME HUD

    [SerializeField]
    GameObject endGameMenu;
    [SerializeField]
    GameObject endGameCanvas;
    [SerializeField]
    GameObject startMenu;
    #endregion


    #region STATS HUD
    Text friendsFoundText;
    Text charismaEarnedText;
    Text friendsFoundText2;
    Text charismaEarnedText2;
    #endregion

    GameObject storyPanel;

    public void InitializeManager()
    {
        friendCount = GameObject.Find("friendCountText").GetComponent<Text>();
        lifeText = GameObject.Find("lifeText").GetComponent<Text>(); ;
        keyInputHUD = GameObject.Find("PressedButtonHUD");   
        keyInputText1 = GameObject.Find("keyInputText1").GetComponent<Text>(); 
        keyInputText2 = GameObject.Find("keyInputText2").GetComponent<Text>();
        
        endGameMenu = GameObject.Find("GameOver");
        
        endGameCanvas = GameObject.Find("MainMenu");
        startMenu = GameObject.Find("StartPanel");
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        friendsFoundText = GameObject.Find("friendsFoundText").GetComponent<Text>();
        charismaEarnedText = GameObject.Find("charismaEarnedText").GetComponent<Text>();

        friendsFoundText2 = GameObject.Find("friendsFoundText (1)").GetComponent<Text>();
        charismaEarnedText2 = GameObject.Find("charismaEarnedText (1)").GetComponent<Text>();

        storyPanel = GameObject.Find("StoryPanel");

        storyPanel.SetActive(false);
        keyInputHUD.SetActive(false);
        endGameMenu.SetActive(false);
    }

    private void Start()
    {
        keyInputAnimationTime = keyInputAnimation.length;
    }

    public void AddFriend()
    {
        PlayerStats.current.currentFriends++;
        friendCount.text = "Friends: " + PlayerStats.current.currentFriends.ToString();
    }


    public void ShowStory(bool show)
    {
        storyPanel.SetActive(show);
    }

    public void AddLife(int amount)
    {
        //Debug.Log("lives before hurt " + PlayerStats.current.currentLives.ToString());
        PlayerStats.current.currentLives += amount;
        if (PlayerStats.current.currentLives > 0)
        {
            lifeText.text = "Confidence: " + PlayerStats.current.currentLives.ToString();
        }
        else
        {
            GameManager.instance.EndGame();
            lifeText.text = "Confidence: " + "0";
        }
        //Debug.Log("lives after hurt " + PlayerStats.current.currentLives.ToString());
    }

    private void Update()
    {
        foreach (char letter in alphabet)
        {
            string letterString = "" + letter;
            if (letterString == "" + Input.inputString.ToLower())
            {
                keyInputHUD.SetActive(true);
                keyInputAnimationEndTime = Time.time + keyInputAnimationTime;
                keyInputText1.text = letterString.ToUpper();
                keyInputText2.text = letterString.ToUpper();

                if (!playerController.inFriendZone)
                    GameManager.instance.audioManager.PlayKeyTapSFX();
            }
        }

        if (keyInputHUD.activeInHierarchy && Time.time > keyInputAnimationEndTime)
        {
            keyInputHUD.SetActive(false);
        }
        
    }

    public void HideMainMenu()
    {
        endGameCanvas.SetActive(false);
        startMenu.SetActive(false);
    }

    public void ShowEndGameUI()
    {
        endGameCanvas.SetActive(true);
        endGameMenu.SetActive(true);

        friendsFoundText.text = "found " + PlayerStats.current.currentFriends + " friends";
        PlayerStats.current.SetEndGameStats();
        charismaEarnedText.text = "earned " + PlayerStats.current.currentFriends + " charisma";

        friendsFoundText2.text = "best: " + PlayerStats.current.friendRecord + " friends";
        charismaEarnedText2.text = "total:  " + PlayerStats.current.currentCharisma + " charisma";
    }
}
