using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterfaceManager : MonoBehaviour
{
    [SerializeField]
    Animator friendTextHUD;
    [SerializeField]
    Text friendText;

    public void ShowNewFriendTextHUD(string newFriendText)
    {
        friendText.text = newFriendText;
        friendTextHUD.SetTrigger("highlight");
    }
}
