using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimanisSkillButton : MonoBehaviour
{
    bool isButtonSelected = false;

    [SerializeField]
    int skillID;
    [SerializeField]
    Sprite buttonDefaultSprite;
    [SerializeField]
    Sprite buttonHighlightSprite;

    #region description
    [SerializeField]
    string skillDescr;
    string skillTitle;
    [SerializeField]
    int skillCost;
    #endregion

    Button skillButton;

        
    private void Start()
    {
        skillButton = gameObject.GetComponent<Button>();
        skillButton.onClick.AddListener(ProcessButtonClick);
        GetButtonData();
    }

    private void GetButtonData()
    {
        foreach (PlayerSkill playerSkill in PlayerStats.current.playerSkills)
        {
            if (playerSkill.skillID == skillID)
            {
                skillTitle = playerSkill.skillName;
            }
        }
    }

    public void ProcessButtonClick()
    {
        
        if (!isButtonSelected)
        {
            skillButton.image.sprite = buttonHighlightSprite;
            //Debug.Log("this is " + skillTitle);
            PlayerStats.current.selectedSkillID = skillID;
            PlayerStats.current.selectedSkillSprite = buttonDefaultSprite;
            isButtonSelected = true;
        }
        else
        {
            DeselectButton();
        }
    }

    private void Update()
    {
        if (PlayerStats.current.selectedSkillID != skillID && isButtonSelected)
        {
            DeselectButton();
        }
    }

    private void DeselectButton()
    {
        skillButton.image.sprite = buttonDefaultSprite;
        isButtonSelected = false;
    }
}
