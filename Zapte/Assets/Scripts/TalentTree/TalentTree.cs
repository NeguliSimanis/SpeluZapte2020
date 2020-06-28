using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalentTree : MonoBehaviour
{
	[SerializeField]
	private Talent[] talents;
	
	[SerializeField]
	private Talent[] unlockedByDefault;
	
	private int points = 5;
	
	[SerializeField]
	private Text talentPointText;
	
    // Start is called before the first frame update
    void Start()
    {
        ResetTalents();
    }
	
	public void TryUseTalent(Talent talent){
		if(MyPoints > 0 && talent.Click()){
			MyPoints--;
		}
		if(MyPoints == 0){
			foreach(Talent t in talents){
				if(t.MyCurrentCount == 0){
					t.Lock();
				}
			}
		}
	}
	
	public int MyPoints{
		get{
			return points;
		}
		set{
			points = value;
			UpdateTalentPointText();
		}
	}

 
	
	//ResetTalents when restart game
	private void ResetTalents(){
		UpdateTalentPointText();
		
		foreach(Talent talent in talents){
			talent.Lock();
			
		}
		
		foreach(Talent talent in unlockedByDefault){
			talent.Unlock();
		}
	}
	
	private void UpdateTalentPointText(){
		talentPointText.text = points.ToString();
	}
}
