using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats 
{
    public static PlayerStats current;

    public int maxLives = 3;
    public int currentLives;

    public int currentFriends;
    public int currentCharisma = 0; //calculated from friends

    public int friendRecord = 0;

    public void Reset()
    {
        currentLives = maxLives;
        currentFriends = 0;
        Debug.Log("Current charisma: " + currentCharisma);
    }

    public void SetEndGameStats()
    {
        currentCharisma += currentFriends;
        if (currentFriends > friendRecord)
            friendRecord = currentFriends;
    }
}
