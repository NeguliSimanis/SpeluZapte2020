using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
   public GameObject obstacle;
   
   public GameObject[] people;
   private int rand;
   private float random;
   private float timeBtwSpawn;
   public float startTimeBtwSpawnMin;
   public float startTimeBtwSpawnMax;
   public float decreaseTime;
   public float minTime = 0.65f;
   private float decrease;
  
   private float startTimeBtwSpawn;

    private void Update(){
        if (!GameManager.instance.gameStarted)
            return;
	   random = Random.Range(startTimeBtwSpawnMin, startTimeBtwSpawnMax);
	   startTimeBtwSpawn = random;
	   if(startTimeBtwSpawn + decrease >= minTime){
		   startTimeBtwSpawn += decrease;
	   }
	   if(timeBtwSpawn <= 0){
		   rand = Random.Range(0, people.Length);
		   Instantiate(people[rand], transform.position, Quaternion.identity);
		   timeBtwSpawn = startTimeBtwSpawn;
		   if(startTimeBtwSpawn > minTime){
			   decrease -= decreaseTime;
		   }
		  
		   
	   }else{
		   timeBtwSpawn -= Time.deltaTime;
	   }
   }
   
   
   //public Transform spawnPoint;
  
   
}
