using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Talent : MonoBehaviour
{
	private Image sprite;
	
	[SerializeField]
	private Text countText;
	
	[SerializeField]
	private int maxCount;
	
	private int currentCount;
	
	[SerializeField]
	private bool unlocked;
	
	[SerializeField]
	private Talent[] childTalent;
	
	[SerializeField]
	private Sprite arrowSpriteLocked;
	
	[SerializeField]
	private Sprite arrowSpriteUnlocked;
	
	[SerializeField]
	private Image[] arrowImage;
	
	private void Awake(){
		sprite = GetComponent<Image>();
		countText.text = $"{currentCount} / {maxCount}";
		
		if(unlocked){
			Unlock();
		}
	}
	
	public bool Click(){
		if(currentCount < maxCount && unlocked){
			currentCount++;
			countText.text = $"{currentCount} / {maxCount}";
			
			if(currentCount == maxCount){
				if(childTalent != null){
					foreach(Talent talent in childTalent){
						talent.Unlock();
					}
				}
			}
			
			return true;
		}
		
		
		return false;
	}
	
	public void Lock(){
		sprite.color = Color.gray;
		countText.color = Color.gray;
		
		if(arrowImage != null){
			foreach(Image image in arrowImage){
				image.sprite = arrowSpriteLocked;
			}
			
		}
		
		if(countText != null){
			countText.color = Color.gray;
		}
		
		
	}
	
	public int MyCurrentCount{
		get{
			return currentCount;
		}
		
		set{
			currentCount = value;
		}
	}
	
	public void Unlock(){
		sprite.color = Color.white;
		countText.color = Color.white;
		
		if(arrowImage != null){
			foreach(Image image in arrowImage){
				image.sprite = arrowSpriteUnlocked;
			}
			
		}
		
		if(countText != null){
			countText.color = Color.white;
		}
		
		unlocked = true;
		
	}
}
