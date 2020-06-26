using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats 
{
    public static PlayerStats current;

    public int maxLives = 3;
    public int currentLives;

    public int currentFriends;


    public void Reset()
    {
        currentLives = maxLives;
        currentFriends = 0;
    }
}
