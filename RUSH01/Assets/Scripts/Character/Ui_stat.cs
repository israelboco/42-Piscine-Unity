using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ui_stat : MonoBehaviour
{

	public GameObject player;
	public Stat player_stat;
	private bool firs_update = true;
	private bool is_open = true;
	public GameObject Ui_obj;
	public int pc_used;
	public int pc;
	public int actual_pc;
	public Text pc_info;

	public List<GameObject> stat_ui;
	private bool need_active_ui = true;
	
	// Use this for initialization
	
	void Start ()
	{
		player = GameObject.FindWithTag("Player");
		player_stat = player.GetComponent<Stat>();
		int i = 0;
		foreach (GameObject ui in stat_ui)
		{
			int k = i;
			Button button = ui.GetComponentInChildren<Button>();
			button.onClick.AddListener(() => click_stat(ui));
			i++;

			name = ui.transform.name;
			if (name == "Str")
			{
				ui.transform.Find("stat").GetComponent<Text>().text = player_stat.STR.ToString();

			}
			else if (name == "Agi")
			{
				ui.transform.Find("stat").GetComponent<Text>().text = player_stat.AGI.ToString() ;

			}
			else if (name == "Con")
			{
				ui.transform.Find("stat").GetComponent<Text>().text = player_stat.CON.ToString() ;

			}
			else if (name == "Armor" && player_stat.ARMOR < 180)
			{
				ui.transform.Find("stat").GetComponent<Text>().text = player_stat.ARMOR.ToString() ;
			}

		}
	}

	void click_stat(GameObject ui)
	{
		name = ui.transform.name;
		print("on click for  = "+ name);
		if (name == "Str")
		{
			player_stat.STR++;
			ui.transform.Find("stat").GetComponent<Text>().text = player_stat.STR.ToString();

		}
		else if (name == "Agi")
		{
			player_stat.AGI++;
			ui.transform.Find("stat").GetComponent<Text>().text = player_stat.AGI.ToString() ;

		}
		else if (name == "Con")
		{
			player_stat.CON++;
			ui.transform.Find("stat").GetComponent<Text>().text = player_stat.CON.ToString() ;

		}
		else if (name == "Armor" && player_stat.ARMOR < 180)
		{
			player_stat.ARMOR++;
			ui.transform.Find("stat").GetComponent<Text>().text = player_stat.ARMOR.ToString() ;

		}
		else
		{
			print("not up");
			return;
		}

		pc_used++;
		player_stat.Reload_stat();

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.C) || firs_update)
		{
			if (firs_update)
				firs_update = false;

			if (is_open == true) // si le menu et ouver on le ferme
			{
				is_open = false;
				Ui_obj.SetActive(false);
				return;
			}
			is_open = true;
			Ui_obj.SetActive(true);
			
		}
		pc = player.GetComponent<Stat>().LV * 5;
		actual_pc = pc - pc_used;
		pc_info.text = "point de capacite a repartire : " + actual_pc.ToString();
		if (actual_pc > 0 && need_active_ui)
		{
			foreach (GameObject ui in stat_ui)
			{
				ui.GetComponentInChildren<Button>().enabled = true;
			}
		}
		else
		{
			foreach (GameObject ui in stat_ui)
			{
				ui.GetComponentInChildren<Button>().enabled = false;
			}
		}

		

	}
}
