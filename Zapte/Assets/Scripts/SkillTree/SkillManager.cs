using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
	public static SkillManager instance; //MARKER singleton Pattern
	
	public Skill[] skills;
	public SkillButton[] skillButtons;
  
	public Skill activateSkill;
  
	private void Awake(){
		if(instance == null){
			instance = this;
		}else{
			if(instance != this){
				Destroy(gameObject);
			}
		}
		DontDestroyOnLoad(gameObject);
		
	}
  
}
