using System.Collections;
using UnityEngine;

public class Human00 : MonoBehaviour {

	public Vector3 target;

	public float speed = 3.0f;
	public float max_distance = 0.0f;

	[SerializeField]
	private Camera cam;

	public ManagerSounds sounds;
	public Animator animator;
	private float footman_z;
	private bool moving;
	private Vector3 mousePos;

	// Use this for initialization
	void Start () {
		if (animator == null)
			animator = GetComponent<Animator> ();
		if (cam == null) {
			cam = Camera.main;
		}
		moving = false;
		animator.speed = 0;
		animator.SetFloat ("Direction", -1);

	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			soundsAction ("Acknowledge");
			this.mousePos = this.cam.ScreenToWorldPoint (Input.mousePosition);
			this.mousePos.z = 0.0f;
			this.target = this.mousePos;

			float direction = Vector3.Dot (Vector3.up, VectDirection (this.mousePos, this.transform.position));

			animatorTrigger ("Walk");
			animatorFloat ("Direction", direction);
			this.transform.localScale = SpriteScaleReverse (this.mousePos.x, this.transform.position.x);
			this.moving = true;
			this.animator.speed = 1;
		}
		if (this.moving) {
			movingCharacter ();
		}
	}

	void StopMooving () {
		this.transform.position = this.target;
		moving = false;
		animator.speed = 0;
		animatorPlay ("Walking");
	}

	bool CheckMoovingDistance(float a, float b){
		return (a > b);
	}
	void movingCharacter () {
		Vector3 displace = VectDirection (mousePos, this.transform.position) * speed * Time.deltaTime;
		float old_distance = (this.transform.position - target).magnitude;
		if(CheckMoovingDistance (old_distance, displace.magnitude))
			this.transform.position += displace;
		else
			 StopMooving ();
	}

	Vector3 SpriteScaleReverse (float target, float pos) {
		return (target < pos ? new Vector3 (-1, 1, 1) : new Vector3 (1, 1, 1));
	}
	Vector3 VectDirection (Vector3 target, Vector3 pos) {
		return (target - pos).normalized;
	}

	void soundsAction (string name) {
		sounds.Play (name);
	}

	void animatorTrigger (string name) {
		animator.SetTrigger (name);
	}

	void animatorPlay (string name) {
		animator.Play (name, 0, 0);
	}
	void animatorFloat (string name, float direction) {
		animator.SetFloat (name, direction);
	}
}