using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private PlayerController playerController;

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
    }

    public void StartGame()
    {
        playerController.StartGame();
    }
}
