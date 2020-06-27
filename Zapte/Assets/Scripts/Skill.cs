using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//MARKER Skill Detail 
public class Skill : MonoBehaviour
{
  public string skillName;
  public Sprite skillSprite;
  private Image sprite;

  [TextArea(1, 3)]
  public string skillDes;
  public bool isUpgrade;
  
  [SerializeField]
  private Text countText;
  
  [SerializeField]
  private int maxCount;
   
  
  private int currentCount;
  
  
  private void Awake(){
	  sprite = GetComponent<Image>();
	  
  }
  
  public bool Click(){
	  if(currentCount < maxCount){
		  currentCount++;
		  countText.text = $"{currentCount} / {maxCount}";
		  return true;
	  }
	  
	  
	  
	  return false;
  }
  
  public void Lock(){
	  sprite.color = Color.gray;
	  countText.color = Color.gray;
  }
  
  public void Unlock(){
	  sprite.color = Color.white;
	  countText.color = Color.white;
  }
  
}
