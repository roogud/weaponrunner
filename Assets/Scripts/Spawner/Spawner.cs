using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Tooltip ("We'll make dupes of this object.")]
    public GameObject spawnedPrefab;

	[Tooltip ("We'll make this powerup appear")]
	public GameObject powerup;

	[Tooltip ("This is the percentage chance the powerup will appear")]
	public float powerupChance; 

	[Tooltip ("We'll make this enemy appear on the prefab")]
	public GameObject[] enemy;
    
    [Tooltip ("If true, the object is spawned with this as its parent. If false, it's spawned within this's parent.")]
    public bool parentToThis = true;

    [Tooltip ("If this exists, then the object will be spawned at the location of this Transform.")]
    public Transform platformSpawnTarget;

	[Tooltip ("If this exists, then the enemy will be spawned at the location of this Transform.")]
	public Transform enemySpawnTarget;

	[Tooltip ("If this exists, then the powerup will be spawned at the location of this Transform.")]
	public Transform powerupSpawnTarget;

    public virtual void Spawn()
    {
        GameObject obj = Instantiate<GameObject>(spawnedPrefab);
		GameObject nme = Instantiate<GameObject> (enemy[Random.Range(0,enemy.Length)]);

//        obj.transform.parent = parentToThis ? transform : transform.parent;
        obj.transform.position = platformSpawnTarget != null ? platformSpawnTarget.position : transform.position;
		nme.transform.position = enemySpawnTarget != null ? enemySpawnTarget.position : transform.position;

		if (Random.Range(0, 100) <= powerupChance) {
			
			GameObject pwrup = Instantiate<GameObject> (powerup);
			pwrup.transform.position = powerupSpawnTarget != null ? powerupSpawnTarget.position : transform.position;
		}
    }
}
