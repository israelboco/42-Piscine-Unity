using UnityEngine;
using System.Collections;

public class BuildingSpwan : MonoBehaviour {

	public ManageSpwanner spawner;

	void OnDestroy() {
		if (spawner)
			spawner.buildingDestroyed ();
	}

}
