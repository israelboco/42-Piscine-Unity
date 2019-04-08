using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class detectionBarScript : MonoBehaviour {

	// Use this for initialization

	public bool catchPlayer = false;
	public float invisible = 0;
	public Scrollbar bar;

	void Start () {
		StartCoroutine ("CheckProgressBar");
	}

	void Update () {

	}



	IEnumerator CheckProgressBar () {
		while (true) {
			if (catchPlayer)
				invisible += 1;
			else
				invisible -= 0.5f;
			yield return new WaitForSeconds (0.05f);
		}
	}

	public void AlarmDetection (float value) {
		invisible += value;
		catchPlayer = true;
	}
	public void DisableAlarmDetection (float value) {
		invisible -= value;
		catchPlayer = false;
	}

	public void ScrollInvisibleBar(){
		if (invisible < 0)
			invisible = 0;
		bar.size = invisible / 100;
		if(invisible >= 75) {
			bar.targetGraphic.color = Color.red;
		} else {
			bar.targetGraphic.color = Color.white;
		}
	}
}