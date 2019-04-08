using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAnimations : MonoBehaviour {

	[SerializeField]
	private Animator animator;
	public ManagerSounds sounds;
	// Use this for initialization

	void Start () {
		if(animator == null)
			animator = GetComponent<Animator>();
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

	public void animatorSetSpeed(float speed){
		animator.speed = speed;
	}
}
