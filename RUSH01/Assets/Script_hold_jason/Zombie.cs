using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{


	public SphereCollider detection;
//	public SphereCollider attack;

	public NavMeshAgent agent;

	public GameObject player_master;

	public GameObject player;
	public Animator anim;
	public bool follow;
	public bool attack;

	public Stat my_stat;

	private bool dead = false;
	
	
	

	// Use this for initialization
//	private void Awake()
//	{
//		
//	}

	IEnumerator ft_dead()
	{
		yield return new WaitForSeconds(4);
		transform.GetComponent<CapsuleCollider>().enabled = false;
		//agent.enabled = false;
		int i = 0;
		while (i < 30)
		{
			yield return new WaitForSeconds(0.5f);
			agent.baseOffset -= 0.002f;
			//transform.Translate(Vector3.down);
			i++;
		}
		Destroy(this.gameObject);
		
	}

	private void OnTriggerEnter(Collider other)
	{
		Debug.Log(other.transform.tag);
		if (other.transform.tag == "Player")
			follow = true;
	}

	void Start () {
		player = GameObject.Find("Maya");
		my_stat = gameObject.GetComponent<Stat>();
		Debug.Log(my_stat.HP);
		anim.SetFloat("life", my_stat.HP);

	}
	
	// Update is called once per frame

	private float life;
	void Update ()
	{
		life = anim.GetFloat("life");
		my_stat.HP = life;
		if (life <= 0)
		{
			if(dead)
				return;
			Click_to_Move.instance.addXp(my_stat.XP);
			dead = true;
			StartCoroutine(ft_dead());
		}

		if (Vector3.Distance(transform.position, player.transform.position) <= 5)
		{
			transform.LookAt(player.transform);
			anim.SetBool("maya_in_range",false);
			follow = false;
			anim.SetBool("maya_in_attack",true);
			return;
		}
		else if(anim.GetBool("maya_in_attack"))
		{
			anim.SetBool("maya_in_attack",false);
			follow = true;
			//return;
		}
		if (follow)
		{
			anim.SetBool("maya_in_range",true);
			agent.destination = player.transform.position;
		}
	}
}
