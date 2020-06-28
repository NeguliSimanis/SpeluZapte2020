using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharismaUI : MonoBehaviour
{
    private Text charismaText;
    void Start()
    {
        charismaText = gameObject.GetComponent<Text>();
    }

    private void FixedUpdate()
    {
        charismaText.text = "charisma: " + PlayerStats.current.currentCharisma;
    }
}
