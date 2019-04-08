using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcAnimations : MonoBehaviour {

	[SerializeField]
	private Animator animator;
	public ManagerSounds sounds;
	// Use this for initialization
	void Awake() {
		// if(animator == null)
			// animator = GetComponent<Animator>();
	}
	void Start () {
		if(animator == null)
			animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void soundsAction (string name) {
		sounds.Play (name);
	}

	public void animatorTrigger (string name) {
		animator.SetTrigger (name);
	}

	public void animatorPlay (string name) {
		animator.Play (name, 0, 0);
	}
	public void animatorFloat (string name, float direction) {
		animator.SetFloat (name, direction);
	}

	public void animatorSetSpeed(int speed){
		animator.speed = speed;
	}
}
