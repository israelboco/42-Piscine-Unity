using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.AI;
public class SpanSpecefique : MonoBehaviour
{

	//public float rad;
	public float time;
	//public float time_factor; // time *(timefacore/lv)
	private bool need_spawn = true;

	public GameObject mob_type_1;
	public GameObject mob_type_2;
	public GameObject player;
	public Stat player_stat;
	private GameObject z;

	private void Start()
	{
		player = GameObject.FindWithTag("Player");
		player_stat = player.GetComponent<Stat>();
		NavMeshHit hit;
		//print(NavMesh.AllAreas);
		if(NavMesh.FindClosestEdge(transform.position, out hit, int.MaxValue))
			transform.position = hit.position;
		else
		{
			print("useless spwan");
		}
	}




	public GameObject actual_zombie;

	// Use this for initialization
	private bool in_wait = false;

	IEnumerator spanw_in_delay(float t)
	{
		in_wait = true;
		yield return new WaitForSeconds(t);
	
		//Vector3 pos = hit.position;
		int r = Random.Range((int) 0, (int) 1);
		Stat s_z;
		if (r == 0)
		{
			//calcule sta zobie lv
			z = Instantiate(mob_type_1,transform.position , Quaternion.identity);
			s_z = z.GetComponent<Stat>();
			s_z.STR = 5 + (float) (player_stat.LV * 2.5);
			s_z.AGI = 5 + player_stat.LV + (float) (player_stat.LV);
			s_z.CON = 5 + player_stat.LV * 5;
			s_z.ARMOR = 5 + player_stat.LV;
			if (s_z.ARMOR > 180)
				s_z.ARMOR = 180;
		}
		else
		{
			z = Instantiate(mob_type_2,transform.position, Quaternion.identity);
			s_z = z.GetComponent<Stat>();
			s_z.STR = 5 + (float) (player_stat.LV * 2.5);
			s_z.AGI = 5 + (float) (player_stat.LV * 1.2);
			s_z.CON = 5 + player_stat.LV * 5;
			s_z.ARMOR = 5 + player_stat.LV;
			if (s_z.ARMOR > 180)
				s_z.ARMOR = 180;

			//calcule sta zobie lv
		}

		actual_zombie = z;
		s_z.LV = player_stat.LV;
		s_z.XP = 100 + player_stat.LV;

		yield return new WaitForSeconds(t);
		in_wait = false;
	}

	// Update is called once per frame
	void Update()
	{
		if (!actual_zombie && in_wait == false)
			StartCoroutine(spanw_in_delay(time));


	}
}
