using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterfaceManager : MonoBehaviour
{
    [SerializeField]
    Text friendCount;

    [SerializeField]
    Text lifeText;

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
            lifeText.text = "Lives: " + "0";
        }
        //Debug.Log("lives after hurt " + PlayerStats.current.currentLives.ToString());
    }
}
