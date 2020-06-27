using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private PlayerController playerController;
    private AudioManager audioManager;

    #region SPEED MANAGEMENT
    float standardGameSpeed = 1f;
    float gameSpeedIncrease = 0.08f;
    float speedIncreaseIncrement = 1f; // detėrmines how often speed is updated
    float nextSpeedIncreaseTime = 0f;
    float maxGameSpeed = 4.4f;
    float fastSpeedMultiplier = 1.5f;
    bool increaseSpeed = false;
    float musicSpeedIncreaseIncrement = 0.2f;
    #endregion

    #region FAST MODE
    [SerializeField]
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
    }

    private void Start()
    {
        if (GameManager.instance == null)
            GameManager.instance = this;
        else 
            Destroy(this.gameObject);
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
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
                Debug.Log("curr speed " + Time.timeScale);
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
                    StartFastMusic();
                    Time.timeScale *= fastSpeedMultiplier;
                }
            }
            //    Debug.Log("MAX SPEED REACHED");
        }
    }

    public void StartGame()
    {
        playerController.StartGame();
        increaseSpeed = true;
       // StartCoroutine(IncreaseSpeed());
    }


    private void UpdateMusicSpeed()
    {
        float musicSpeed = 1f + (Time.timeScale - 1f) * musicSpeedIncreaseIncrement;
        audioManager.SetMusicSpeed(musicSpeed);
    }



    private void StartFastMusic()
    {
        fastMusicPlaying = true;
        audioManager.PlayFastMusic();
        fastModeImage.SetActive(true);
    }
}
