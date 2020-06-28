using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill
{
    public string skillName;
    public int skillID;
    public int cost;
    public bool purchased = false;
    public bool availableForPurchase;
    public string description;
    public int requiredSkillID;

    public PlayerSkill (string name, int ID, int skilCost, bool available, string skillDescription, int requiredSkill)
    {
        skillID = ID;
        skillName = name;
        cost = skilCost;
        availableForPurchase = available;
        description = skillDescription;
        requiredSkillID = requiredSkill;
    }
}


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
    public bool gainConfidenceOnJump = false;
    public int currentJumpID = 0;
    public int jumpConfidenceGainFrequency = 3;

    // AoE FRIEND GAIN
    public bool gainMultipleFriendsOnButtonPress = false;
    public float gainMultipleFriendsChance = 0.2f;
    public bool multipleFriendsGainEnabled = false;

    // chance to gain life on friend
    public bool canGainLifeOnFriend = false;
    public float gainLifeFromFriendChance = 0.1f;

    public List<PlayerSkill> playerSkills = new List<PlayerSkill>();
    public int selectedSkillID = 1;
    public Sprite selectedSkillSprite;
    #endregion

    public PlayerStats()
    {
        playerSkills.Add(new PlayerSkill("Confidence", 1, 3, true, "Increase your max confidence by 1", 0));
        playerSkills.Add(new PlayerSkill("Exercise I", 2, 10, false, "Gain 1 confidence on every eighth jump", 1));
        playerSkills.Add(new PlayerSkill("Public Speaker I", 3, 8, false, "10% chance to gain an additional friend", 1));
        playerSkills.Add(new PlayerSkill("Elevating Confidence", 4, 15, false, "Increase your max confidence by 1", 2));
        playerSkills.Add(new PlayerSkill("Everpresent Confidence", 5, 15, false, "Increase your max confidence by 1", 3));
        playerSkills.Add(new PlayerSkill("Exercise II", 6, 30, false, "Gain 1 confidence on every fifth jump", 4));
        playerSkills.Add(new PlayerSkill("Public Speaker II", 7, 30, false, "30% additional chance to gain an additional friend", 5));

        playerSkills.Add(new PlayerSkill("Positive Reinforcement I", 8, 20, false, "10% chance to gain 1 confidence when you find a friend", 3));
        playerSkills.Add(new PlayerSkill("Positive Reinforcement II", 9, 40, false, "20% additional chance to gain 1 confidence when you find a friend", 8));
    }

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

    public void UnlockSkill(int unlockedSkillID)
    {
        if (playerSkills[unlockedSkillID].purchased)
            return;
        Debug.Log("pruchased " + playerSkills[unlockedSkillID].skillName);
        playerSkills[unlockedSkillID].purchased = true;

        switch (unlockedSkillID)
        {
            case 1:
                maxLives++;
                break;
            case 2:
                gainConfidenceOnJump = true;
                jumpConfidenceGainFrequency = 7;
                break;
            case 3:
                gainMultipleFriendsOnButtonPress = true;
                gainMultipleFriendsChance = 0.1f;
                break;
            case 4:
                maxLives++;
                break;
            case 5:
                maxLives++;
                break;
            case 6:
                gainConfidenceOnJump = true;
                jumpConfidenceGainFrequency = 4;
                break;
            case 7:
                gainMultipleFriendsOnButtonPress = true;
                gainMultipleFriendsChance = 0.4f;
                break;
            case 8:
                canGainLifeOnFriend = true;
                gainLifeFromFriendChance = 0.1f;
                break;
            case 9:
                canGainLifeOnFriend = true;
                gainLifeFromFriendChance = 0.3f;
                break;
        }
    }
}
