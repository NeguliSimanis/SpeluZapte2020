using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public bool gameStarted = false;
    public static GameManager instance;
    private PlayerController playerController;


    [HideInInspector]
    public AudioManager audioManager;
    private UserInterfaceManager userInterfaceManager;

    #region SPEED MANAGEMENT
    float standardGameSpeed = 1f;
    float gameSpeedIncrease = 0.08f;
    float speedIncreaseIncrement = 1f; // detėrmines how often speed is updated
    float defaultSpeedIncreaseIncrement = 1F;
    float nextSpeedIncreaseTime = 0f;
    float maxGameSpeed = 4.4f;
    float fastSpeedMultiplier = 1.5f;
    bool increaseSpeed = false;
    float musicSpeedIncreaseIncrement = 0.2f;
    #endregion

    #region FAST MODE
    GameObject fastModeImage;
    float fastMusicStartDelay = 24f;
    float fastMusicStartTime;
    bool fastMusicPlaying = false;
    bool fastMusicStartTimeSet = false;
    #endregion

    private void Awake()
    {
        if (PlayerStats.current == null)
        {
            PlayerStats.current = new PlayerStats();
            DontDestroyOnLoad(this);
        }
        audioManager = transform.GetChild(0).gameObject.GetComponent<AudioManager>();
     
        if (GameManager.instance == null)
            GameManager.instance = this;
        else
            Destroy(this.gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetGame();
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void ResetGame()
    {
        Time.timeScale = standardGameSpeed;
        PlayerStats.current.Reset();
        increaseSpeed = false;
        fastMusicPlaying = false;
        gameStarted = false;

        fastModeImage = GameObject.Find("FastMusicOverlay");
        fastModeImage.SetActive(false);
        userInterfaceManager = gameObject.GetComponent<UserInterfaceManager>();
        userInterfaceManager.InitializeManager();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && PlayerStats.current.currentLives > 0)
        {
            StartGame(); 
        }

        if (!increaseSpeed)
            return;
        if (nextSpeedIncreaseTime == 0 || nextSpeedIncreaseTime < Time.time)
        {
            speedIncreaseIncrement *= 1.1f;
            nextSpeedIncreaseTime = Time.time + speedIncreaseIncrement;

            if (Time.timeScale < maxGameSpeed)
            {
                Time.timeScale *= (1 + gameSpeedIncrease);
                UpdateMusicSpeed();
                //Debug.Log("curr speed " + Time.timeScale);
            }

            else 
            {
                if (!fastMusicStartTimeSet)
                {
                    fastMusicStartTimeSet = true;
                    fastMusicStartTime = Time.time + fastMusicStartDelay;
                }
                if (!fastMusicPlaying && Time.time > fastMusicStartTime)
                {
                    StartFastMusic(true);
                    Time.timeScale *= fastSpeedMultiplier;
                }
            }
            //    Debug.Log("MAX SPEED REACHED");
        }
    }

    public void StartGame()
    {
        userInterfaceManager.HideMainMenu();
        playerController.StartGame();
        nextSpeedIncreaseTime = Time.time + speedIncreaseIncrement;
        speedIncreaseIncrement = defaultSpeedIncreaseIncrement;
        fastMusicStartTimeSet = false;
        increaseSpeed = true;
        gameStarted = true;
    }


    private void UpdateMusicSpeed()
    {
        float musicSpeed = 1f + (Time.timeScale - 1f) * musicSpeedIncreaseIncrement;
        audioManager.SetMusicSpeed(musicSpeed);
    }

    private void StartFastMusic(bool start)
    {
        fastMusicPlaying = start;
        audioManager.PlayFastMusic(start);
        fastModeImage.SetActive(start);
    }

    public void EndGame()
    {
        if (!gameStarted)
            return;
        gameStarted = false;
        increaseSpeed = false;
        userInterfaceManager.ShowEndGameUI();
    }

    public void RestartGame()
    {
        StartFastMusic(false);
        audioManager.SetMusicSpeed(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
