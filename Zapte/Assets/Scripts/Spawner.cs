using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
   public GameObject obstacle;
   
   public GameObject[] people;
   private int rand;
   private float timeBtwSpawn;
   public float startTimeBtwSpawn;
   public float decreaseTime;
   public float minTime = 0.65f;
   
   private void Update(){
	   if(timeBtwSpawn <= 0){
		   rand = Random.Range(0, people.Length);
		   Instantiate(people[rand], transform.position, Quaternion.identity);
		   timeBtwSpawn = startTimeBtwSpawn;
		   if(startTimeBtwSpawn > minTime){
			   startTimeBtwSpawn -= decreaseTime;
		   }
		  
		   
	   }else{
		   timeBtwSpawn -= Time.deltaTime;
	   }
   }
   
   
   //public Transform spawnPoint;
  
   
}
