using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//MARKER Skill Detail 
public class Skill : MonoBehaviour
{
  public string skillName;
  public Sprite skillSprite;
  
  [TextArea(1, 3)]
  public string skillDes;
  public bool isUpgrade;
  
}
