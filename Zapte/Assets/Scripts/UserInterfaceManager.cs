using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UserInterfaceManager : MonoBehaviour
{
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


    public void InitializeManager()
    {
        friendCount = GameObject.Find("friendCountText").GetComponent<Text>();
        lifeText = GameObject.Find("lifeText").GetComponent<Text>(); ;
        keyInputHUD = GameObject.Find("PressedButtonHUD");   
        keyInputText1 = GameObject.Find("keyInputText1").GetComponent<Text>(); 
        keyInputText2 = GameObject.Find("keyInputText2").GetComponent<Text>();
        keyInputHUD.SetActive(false);
        endGameMenu = GameObject.Find("GameOver");
        endGameMenu.SetActive(false);
        endGameCanvas = GameObject.Find("MainMenu");
        startMenu = GameObject.Find("StartPanel"); 
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


    public void AddLife(int amount)
    {
        //Debug.Log("lives before hurt " + PlayerStats.current.currentLives.ToString());
        PlayerStats.current.currentLives += amount;
        if (PlayerStats.current.currentLives > 0)
        {
            lifeText.text = "Lives: " + PlayerStats.current.currentLives.ToString();
        }
        else
        {
            GameManager.instance.EndGame();
            lifeText.text = "Lives: " + "0";
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
    }
}
