using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTree : MonoBehaviour
{
	private int points = 1;
	
	
	[SerializeField]
	private Skill[] skills;
	
	[SerializeField]
	private Skill[] unlockedByDefault;
	
	[SerializeField]
	private Text skillPointText;
	
	void Start(){
		ResetSkills();
	}
	
	public void TryUseSkill(Skill skill){
		if(MyPoints > 0 && skill.Click()){
			MyPoints--;
		}
	}
	
	public int MyPoints{
		get{
			return points;
		}
		set{
			points = value;
			UpdateSkillPointText();
		}
	}
	
	
	
	
	private void ResetSkills(){
		
		UpdateSkillPointText();
		
		foreach(Skill skill in skills){
			skill.Lock();
		}
		
		foreach(Skill skill in unlockedByDefault){
			skill.Unlock();
		}
	}
	
	private void UpdateSkillPointText(){
		skillPointText.text = points.ToString();
		
		
	}
}
