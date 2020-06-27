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

    #region SKILLS
    // CONFIDENCE ON JUMP
    public bool gainConfidenceOnJump = true;
    public int currentJumpID = 0;
    public int jumpConfidenceGainFrequency = 3;

    // AoE FRIEND GAIN
    public bool gainMultipleFriendsOnButtonPress = true;
    public float gainMultipleFriendsChance = 0.2f;
    public bool multipleFriendsGainEnabled = false;
    #endregion

    public void Reset()
    {
        currentLives = maxLives;
        currentFriends = 0;
        currentJumpID = -1;
    }

    public void SetEndGameStats()
    {
        currentCharisma += currentFriends;
        if (currentFriends > friendRecord)
            friendRecord = currentFriends;
    }
}
