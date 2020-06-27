using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartGameButton : MonoBehaviour
{
     public void Restart()
    {
        GameManager.instance.RestartGame();
    }
}
