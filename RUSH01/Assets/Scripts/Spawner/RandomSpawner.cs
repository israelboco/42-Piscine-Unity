using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class RandomSpawner : MonoBehaviour
{

	public float rad;
	public float time;
	public float time_factor; // time *(timefacore/lv)
	private bool need_spawn = true;

	public GameObject mob_type_1;
	public GameObject mob_type_2;
	public GameObject player;
	public Stat player_stat;

	private List<GameObject> z_list = new List<GameObject>();
	private GameObject z;

	private bool reload_lst = true;

	private void Start()
	{
		player = GameObject.FindWithTag("Player");
		player_stat =  player.GetComponent<Stat>();
	}

	
	
	
	public Vector3 RandomNavmeshLocation(float radius) {
		Vector3 randomDirection = Random.insideUnitSphere * radius;
		randomDirection += transform.position;
		NavMeshHit hit;
		Vector3 finalPosition = Vector3.zero;
		if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1)) {
			finalPosition = hit.position;            
		}
		return finalPosition;
	}


	IEnumerator spawnRandom(float t)
	{
		need_spawn = false;
		Vector3 pos = RandomNavmeshLocation(rad);
		int r = Random.Range((int) 0, (int) 1);
		Stat s_z;
		if (r == 0)
		{
			//calcule sta zobie lv
			z = Instantiate(mob_type_1, pos,Quaternion.identity);
			s_z = z.GetComponent<Stat>();
			s_z.STR = 5 + (float) (player_stat.LV * 20);
			s_z.AGI = 5 +  (float) (player_stat.LV * 0.8);
			s_z.CON = 5 + player_stat.LV * 4;
			s_z.ARMOR = 5 + player_stat.LV;
			if (s_z.ARMOR > 50)
				s_z.ARMOR = 50;
		}
		else
		{
			z = Instantiate(mob_type_2, pos,Quaternion.identity);
			 s_z = z.GetComponent<Stat>();
			s_z.STR = 5 + (float) (player_stat.LV * 20);
			s_z.AGI = 5 + (float) (player_stat.LV * 0.8);
			s_z.CON = 5 + player_stat.LV * 4;
			s_z.ARMOR = 5 + player_stat.LV;
			if (s_z.ARMOR > 50)
				s_z.ARMOR = 50;

			//calcule sta zobie lv
		}
		z_list .Add(z);
		s_z.LV = player_stat.LV;
		s_z.XP = 100 + player_stat.LV;
		
		yield return new WaitForSeconds(t);
		need_spawn = true;
	}

	IEnumerator cheked_list()
	{
		reload_lst = false;
		for (int i = 0; i < z_list.Count; i++)
		{
			if (!z_list[i])
			{
				print("remove_dead");
				z_list.Remove(z_list[i]);
			}
		}

		reload_lst = true;
		yield return null;
	}
	
	void Update()
	{
		
		if(need_spawn && z_list.Count < 75)
			StartCoroutine(spawnRandom(time * (time_factor/player_stat.LV)));
		else if(reload_lst)
		{
			StartCoroutine(cheked_list());

		}
		

	}
	
	
}
