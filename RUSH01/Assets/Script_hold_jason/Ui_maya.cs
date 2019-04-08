using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ui_maya : MonoBehaviour
{


	//public GameObject hp_slide;
//	public Slider hp_slide;

//	public Slider xp_slide;
	public Slider hp_slide;
	public Slider xp_slide;

	public Text hp_text;	
	public Text xp_text;
	public Text nv_text;


	public Stat maya_stat;

	private float nxt_level;


	public Slider hp_mob;
	public Text name_mob;

	// Use this for initialization
	
	

	void Start ()
	{
		//maya_stat = Click_to_Move.instance.transform.GetComponent<Stat>();
		hp_slide.maxValue = maya_stat.Max_HP;
		//hp_slide.GetComponent<Slider>().maxValue = maya_stat.HP;
		nxt_level = Click_to_Move.instance.next_lv;
		xp_slide.maxValue = nxt_level;


	}
	
	// Update is called once per frame
	void Update ()
	{
		nxt_level = Click_to_Move.instance.next_lv;

		hp_mob.enabled = false;
//		Debug.Log(maya_stat.HP);
		//hp_slide.GetComponent<Slider>().maxValue = maya_stat.HP;
		hp_slide.maxValue = maya_stat.Max_HP;
//		hp_slide.maxValue = maya_stat.Max_HP;
		hp_slide.value = maya_stat.HP;
		hp_text.text = maya_stat.HP.ToString();
		nv_text.text = "Niv : " + maya_stat.LV.ToString();
		xp_slide.maxValue = nxt_level;
		xp_slide.value = maya_stat.XP;
		xp_text.text = maya_stat.XP.ToString() + " / " + nxt_level.ToString();

		GameObject target = Click_to_Move.instance.target;
		
		if (target && target.transform.GetComponent<Stat>().HP > 0)
		{
			hp_mob.enabled = true;
			Debug.Log("coucou");
			Stat hit_Stat = target.transform.GetComponent<Stat>();
			hp_mob.maxValue = hit_Stat.Max_HP;
			hp_mob.value = hit_Stat.HP;
			name_mob.text = target.transform.name;
			return;
		}
		
		LayerMask mask = ~LayerMask.GetMask("none");
		RaycastHit hit;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, mask,
			QueryTriggerInteraction.Ignore))
		{
			if (hit.transform.tag == "mob")
			{
				hp_mob.enabled = true;
				Stat hit_Stat = hit.transform.GetComponent<Stat>();
				hp_mob.maxValue = hit_Stat.Max_HP;
				hp_mob.value = hit_Stat.HP;
				name_mob.text = hit.transform.name;
				return ;
			}
			
		}
	}
}
