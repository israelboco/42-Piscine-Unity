using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.AI;
public class Click_to_Move : MonoBehaviour {

	NavMeshAgent agent;

	public Stat maya_stat;
	public GameObject maya;
	private Animator maya_anim;

	public GameObject target;

	private bool boton_not_realease = false;
	private  bool domage_apl = false;

	public float next_lv;
	public static Click_to_Move instance;

	private void Awake()
	{
		instance = this;
	}

	
	
	void Start() {
		agent = maya.GetComponent<NavMeshAgent>();
		maya_anim = maya.GetComponent<Animator>();
		//maya_stat = transform.GetComponent<Stat>();
		//maya_stat.XP = 0;
//		Debug.Log(maya_stat.transform.name);

	}
	
	public void addXp(float xp)
	{
//		maya_stat = transform.GetComponent<Stat>();
	//	Debug.Log(maya_stat);
		maya_stat.XP += xp;
		if (maya_stat.XP >= next_lv)
		{
			maya_stat.XP -= next_lv;
			maya_stat.LV++;
			next_lv = next_lv * 1.5f;
			
		}
	}

//	 public void domage()
//	{
//		Debug.Log("im called");
//		Animator t_a = target.GetComponent<Animator>();
//		t_a.SetInteger("life",t_a.GetInteger("life") - 1);
//	}

	IEnumerator domage(int domage, GameObject cible,float t)
	{
		domage_apl = true;
		Stat t_s = cible.GetComponent<Stat>();
		Debug.Log( maya_stat.AGI - t_s.AGI);
		yield return new WaitForSeconds(t); // wait for annimation
		if (Random.Range(0, 100) >= 75 + maya_stat.AGI - t_s.AGI)
		{
			Animator t_a = cible.GetComponent<Animator>();
			float baseDamage =  Random.Range(maya_stat.minDamage, maya_stat.maxDamage);
			baseDamage = baseDamage * (1 - t_s.ARMOR / 200);
			t_a.SetFloat("life", t_a.GetFloat("life") -  baseDamage);
		}

		domage_apl = false;
	}
        
	void Update() {
		if (Input.GetMouseButton(0)) {
			RaycastHit hit;
			LayerMask mask = ~LayerMask.GetMask("none");
//			hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), Mathf.Infinity, mask,QueryTriggerInteraction.Ignore);
	           
			//if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity)) {
			if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit ,Mathf.Infinity, mask,QueryTriggerInteraction.Ignore))
				{
					if (boton_not_realease == false)
					{
						if (Vector3.Distance(maya.transform.position, agent.destination) > 5 ||
						    hit.transform.tag != "mob")
						{
							agent.destination = hit.point;
							maya_anim.SetBool("run", true);
							//maya_anim.SetBool("maya_attack", false);
							//maya_anim.SetTrigger("attack");

//							if (hit.transform.tag == "mob")
//								boton_not_realease = true;
						}
						 if (hit.transform.tag == "mob" && Vector3.Distance(maya.transform.position, agent.destination) < 5 )
						{
							maya_anim.SetBool("run", false);
							//maya_anim.SetBool("maya_attack", true);
//							print(Vector3.Distance(maya.transform.position, agent.destination));
							maya_anim.SetTrigger("attack");
							target = hit.transform.gameObject;
							if (domage_apl == false)
								StartCoroutine(domage(1, target, 0.20f));

							boton_not_realease = true;

						}
					}else if (Vector3.Distance(maya.transform.position, target.transform.position) > 5)
					{
						agent.destination = target.transform.position;
						maya_anim.SetBool("run", true);
//						maya_anim.SetTrigger("attack");
//						if (domage_apl == false)
//							StartCoroutine(domage(1, target, 0.10f));


						//maya_anim.SetBool("maya_attack", false);
					}
					else
					{
//						print(Vector3.Distance(maya.transform.position, target.transform.position));
						maya_anim.SetTrigger("attack");
						maya.transform.LookAt(target.transform);
						if (domage_apl == false)
							StartCoroutine(domage(1, target, 0.20f));

					}


				}
			}
		else
		{
			target = null;
		}
		if(Input.GetMouseButtonUp(0))
			boton_not_realease = false;

		//}
//		Debug.Log(Vector3.Distance(transform.position,agent.destination));
		if(Vector3.Distance(maya.transform.position,agent.destination) <= 2)
			maya_anim.SetBool("run",false);
	}
}
