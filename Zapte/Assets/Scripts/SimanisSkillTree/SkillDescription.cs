using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillDescription : MonoBehaviour
{
    int selectedSkillID = 1;

    [SerializeField]
    Text skillCost;

    [SerializeField]
    Button purchaseButton;

    [SerializeField]
    Image skillImage;

    [SerializeField]
    Text skillTitle;

    [SerializeField]
    Text skillDescription;

    [SerializeField]
    GameObject skillUnavailable;

    [SerializeField]
    GameObject skillPurchased;

    void Start()
    {
        purchaseButton.onClick.AddListener(ProcessButtonClick);
    }


    void FixedUpdate()
    {
        if (PlayerStats.current.selectedSkillID != selectedSkillID)
        {
            selectedSkillID = PlayerStats.current.selectedSkillID;
            UpdateSkillDescr();
        }

        #region SKILL PURCHASED UI
        if (!PlayerStats.current.playerSkills[selectedSkillID - 1].purchased && skillPurchased.activeInHierarchy)
        {
            skillPurchased.SetActive(false);
        }
        else if (PlayerStats.current.playerSkills[selectedSkillID - 1].purchased)
        {
           skillPurchased.SetActive(true);
        }
        #endregion

        #region SKILL leared UI
        int requiredSkillID = PlayerStats.current.playerSkills[selectedSkillID - 1].requiredSkillID;
        bool requiredSkillLearned = true;
        if (requiredSkillID != 0 && PlayerStats.current.playerSkills[requiredSkillID-1].purchased == false)
        {
            requiredSkillLearned = false;
        }
        Debug.Log("required skill ID " + requiredSkillID + ". ");
        if (PlayerStats.current.currentCharisma < PlayerStats.current.playerSkills[selectedSkillID - 1].cost
            || !requiredSkillLearned)
        {

            skillUnavailable.SetActive(true);
        }
        else
        {
            skillUnavailable.SetActive(false);
        }
        if (PlayerStats.current.playerSkills[selectedSkillID - 1].purchased)
        {
            skillUnavailable.SetActive(false);
        }
        #endregion

    }

    public void ProcessButtonClick()
    {
        if (skillPurchased.activeInHierarchy || PlayerStats.current.currentCharisma < PlayerStats.current.playerSkills[selectedSkillID - 1].cost)
            return;
        PlayerStats.current.currentCharisma -= PlayerStats.current.playerSkills[selectedSkillID - 1].cost;
        PlayerStats.current.UnlockSkill(selectedSkillID-1);
        skillPurchased.SetActive(true);
    }

    void UpdateSkillDescr()
    {
        skillCost.text = "cost: " + PlayerStats.current.playerSkills[selectedSkillID - 1].cost;
        skillDescription.text = PlayerStats.current.playerSkills[selectedSkillID - 1].description;
        skillImage.sprite = PlayerStats.current.selectedSkillSprite;
        skillTitle.text = PlayerStats.current.playerSkills[selectedSkillID - 1].skillName;
    }
}
