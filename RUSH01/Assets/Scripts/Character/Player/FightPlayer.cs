using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class FightPlayer : MonoBehaviour {

	private GameObject target;
	public GameObject Target { get { return target; } set { target = value; } }

//	[SerializeField] private float life = 100f;
//	[SerializeField] private float damage = 1f;
	[SerializeField] private float cooldown = 2f;
	[SerializeField] private float rangeAttack = 2f;
	[SerializeField] private float cooldownGlobal;
	[SerializeField] private Animator animator;
	[SerializeField] private GameObject particleLvlUp;
	public GameObject lost_screen;

	private bool die;

	public Stat stat;

	public float next_lv = 100;

	public Image lifebar;
	public Text lifeText;
	public Image xpbar;
	public Text xpText;

	public GameObject enemyBarGameObject;
	public Image enemyScrollbar;
	public Text enemyBarText;


	// public float HP = 20;
	// public float minDamage_base = 5;
	// public float minDamage = 5;
	// public float maxDamage_base = 10;
	// public float maxDamage = 10;
	// public int spendPoints = 0;
	// public float maxHP;
	// [HideInInspector] public int Level = 1;
	// public float XP = 0;
	// [HideInInspector] public float Money = 0;
	// [HideInInspector] public float XPToNextLevel = 50;




	private bool InRangeAttack { get { return Vector3.Distance (transform.position, target.transform.position) < rangeAttack; } }

	private bool isAttack;
	public bool IsAttack { set{ isAttack = value; }  get { return isAttack; } }

	// Use this for initialization
	void Start ()
	{
		stat = GetComponent<Stat>();
		animator = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {
		// Debug.Log (target);

		if(stat.HP > stat.Max_HP) {
			stat.HP = stat.Max_HP;
		}else if (stat.HP <= 0)
		{
			if (die == false)
			{
				lost_screen.SetActive(true);
				die = true;
			}

			if (Input.GetKeyDown(KeyCode.Return))
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			}

			return;
		}

		if(Input.GetKeyDown(KeyCode.Y)){
			CheatLevelUp();
		}
		Attack ();
		if (isAttack) {
			animator.SetBool ("Idle", false);
		}
		InfoGui();
	}


	private void InfoGui(){
		lifebar.fillAmount = stat.HP / stat.Max_HP;
		xpbar.fillAmount = stat.XP / next_lv;
		lifeText.text = ((int)stat.HP).ToString() + " / " + ((int)stat.Max_HP).ToString();
		xpText.text = "Level " + stat.LV.ToString ();
		if (target != null && target.GetComponent<Stat>().HP > 0) {
			enemyBarGameObject.SetActive(true);
			enemyBarText.text = ((int)target.GetComponent<Stat> ().HP).ToString () + " / " + ((int)target.GetComponent<Stat> ().Max_HP).ToString () + " - Level " + target.GetComponent<Stat>().LV.ToString();
			enemyScrollbar.fillAmount = target.GetComponent<Stat> ().HP / target.GetComponent<Stat> ().Max_HP;
		}
		else
			enemyBarGameObject.SetActive(false);

	}
	private void Attack () {
		if (target != null && cooldownGlobal <= Time.time && InRangeAttack) {
			isAttack = true;
			StartCoroutine ("RotationPlayer");
			cooldownGlobal = Time.time + cooldown;
			animator.SetBool ("Attack", true);
			animator.SetBool ("Idle", false);
			animator.SetBool ("Run", false);
		}
	}
	
	
	
	public void Hit () {
		if (Random.Range(0, 100) <= 75 + stat.AGI - target.GetComponent<Stat>().AGI)
		if (target != null) {
			if (!target.GetComponent<MobsControl> ().IsDead) {
				float baseDamage =  Random.Range(stat.minDamage, stat.maxDamage);
				target.GetComponent<MobsControl> ().GetHit (baseDamage * (1 -target.GetComponent<Stat>().ARMOR / 200));
			}
		}
	}

	public void EndAnimation () {
		isAttack = false;
		animator.SetBool ("Attack", false);
	}

	public void GetHit (float damage) {
		stat.HP -= damage;
		if (stat.HP <= 0) {
			stat.HP = 0;
		}
		// Debug.Log (life);
	}
	
	private void CheatLevelUp(){
		stat.XP -= next_lv;
		stat.LV++;
		next_lv = next_lv * 1.5f;
	}


	public void addXp(float xp)
	{
//		maya_stat = transform.GetComponent<Stat>();
		//	Debug.Log(maya_stat);
		stat.XP += xp;
		if (stat.XP >= next_lv)
		{
			while (stat.XP > next_lv)
			{
				stat.XP -= next_lv;
				stat.LV++;
				Destroy(Instantiate(particleLvlUp, transform.position, transform.rotation) as GameObject, 1f);
				// lvlUp.transform.parent = transform;
				// Destroy(lvlUp, 0.5f);
				// Destroy(Instantiate(particleLvlUp, transform.parent.position, transform.parent.rotation), 5);
				// Destroy(Instantiate(particleLvlUp, transform.position, transform.rotation), 3f);
			}
			next_lv = next_lv * 1.5f;
			
		}
	}



	private IEnumerator RotationPlayer () {
		while (isAttack && target != null) {
			Quaternion newRotation = Quaternion.LookRotation (target.transform.position - transform.position);
			transform.rotation = Quaternion.Slerp (transform.rotation, newRotation, 0.2f);
			yield return null;
		}
	}



	private void OnDrawGizmos () {
		// Gizmos.color = Color.red;
		// Gizmos.DrawWireSphere (transform.position, rangeAttack);
	}



	// IEnumerator regainLife() {
	// 	while (true) {
	// 		if (HP < maxHP)
	// 			HP += 1;
	// 		yield return new WaitForSeconds (2f);
	// 	}
	// }
}