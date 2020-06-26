using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private PlayerController playerController;

    float standardGameSpeed = 1f;
    float minGameSpeedIncrease = 0.1f;
    float speedIncreaseIncrement = 1f;
    float nextSpeedIncreaseTime = 0f;
    float maxGameSpeed = 5f;
    bool increaseSpeed = false;

    private void Awake()
    {
        if (PlayerStats.current == null)
            PlayerStats.current = new PlayerStats();
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
            speedIncreaseIncrement *= 1.07f;
            nextSpeedIncreaseTime = Time.time + speedIncreaseIncrement;

            if (Time.timeScale < maxGameSpeed)
            {
                Time.timeScale *= (1 + minGameSpeedIncrease);
                //Debug.Log("curr speed " + Time.timeScale);
            }
            //else
            //    Debug.Log("MAX SPEED REACHED");
        }
    }

    public void StartGame()
    {
        playerController.StartGame();
        increaseSpeed = true;
        //StartCoroutine(IncreaseSpeed());
    }

    private IEnumerator IncreaseSpeed()
    {
        Debug.Log("corot started " + Time.time);
        int frameCount = 0;
        while (frameCount > 20)
        {
            frameCount--;
            yield return null;
        }
        if (Time.timeScale < maxGameSpeed)
        {
            Time.timeScale *= (1 + minGameSpeedIncrease);
            Debug.Log("curr speed " + Time.timeScale);
        }
        else
            Debug.Log("MAX SPEED REACHED");
    }
}
