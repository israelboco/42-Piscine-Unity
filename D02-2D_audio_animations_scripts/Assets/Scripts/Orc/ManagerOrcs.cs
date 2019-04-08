using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerOrcs : MonoBehaviour {

	// Use this for initialization
	public static ManagerOrcs instance { get; private set; }

	public AttackEntity castle;

	public List<AttackEntity> listCtrl = new List<AttackEntity> ();

	public List<HumanController> hitList;
	public List<OrcController> allOrcs;

	
	void Awake() {
		instance = this;
		hitList = new List<HumanController> ();
		allOrcs = new List<OrcController> ();
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		allOrcs.RemoveAll(OrcIsDead);
		listCtrl.RemoveAll(Defait);
		int victim_count = listCtrl.Count;
		if (victim_count > 0) {
			foreach (OrcController orc in allOrcs) 
				if (orc.entityTarget == null) orc.entityTarget = listCtrl[Random.Range(0, victim_count)];
		}
		if (castle.hitPoints < 71) {
			hitList.RemoveAll(HumanIsDead);
			int orc_index = allOrcs.Count -1;
			foreach (HumanController human in hitList) {
				if (orc_index < 0)
					break;
				if (human.entitySelect == castle) {
					allOrcs[orc_index].entityTarget = human.GetComponent<AttackEntity>();
					orc_index--;
				}
			}
		}
	}

	public void Add(OrcController orc) {
		allOrcs.Add (orc);
	}

	public void AddToFightList(HumanController human) {
		hitList.Add (human);
	}
	
	public bool OrcIsDead(OrcController orc) {
		return (orc == null);
	}

	public bool HumanIsDead(HumanController human) {
		return (human == null);
	}

	public bool Defait(AttackEntity victim) {
		return (victim == null);
	}
}
