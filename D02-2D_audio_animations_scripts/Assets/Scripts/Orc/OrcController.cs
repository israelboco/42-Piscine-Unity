using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcController : MonoBehaviour {
	public bool human = false;
	public bool orc = false;
	public bool dead;

	public float speed = 3.0f;
	public float attack_distance = 0.1f;
	public float ditance_trigger = 0.0f;

	public OrcAnimations asset;
	public AttackEntity entityTarget;

	// private Animator animator;

	private bool new_target;
	private bool moving;
	private bool attackTarget;
	private Vector3 targetClick;

	// Use this for initialization
	void Start () {
		dead = false;
		new_target = false;
		moving = false;
		entityTarget = null;
		attackTarget = false;
		asset.animatorFloat ("Direction", -1);
		if (orc) {;
			ManagerOrcs.instance.Add (this);
		}
	}

	void SetAnimationAndTargeting () {
		this.new_target = false;
		this.entityTarget = null;
		this.attackTarget = false;
		this.asset.soundsAction ("Acknowledge");
		this.asset.animatorPlay ("Walking");
		this.asset.animatorTrigger ("Walk");
		this.asset.animatorSetSpeed (1);
		this.moving = true;
	}

	void SelectTarget () {
		this.moving = true;
		this.attackTarget = true;
		this.targetClick = entityTarget.transform.position;
		this.targetClick.z = 0;
	}
	// Update is called once per frame
	void Update () {
		if (this.dead) return;
		if (this.new_target) SetAnimationAndTargeting ();
		if (this.entityTarget) SelectTarget ();
		if (this.attackTarget && this.entityTarget == null) {
			this.attackTarget = false;
			this.moving = false;
		}
		if (this.moving) MovingCharacter ();
		if (!this.moving) {
			this.asset.animatorSetSpeed (0);
			this.asset.animatorPlay ("Walking");
		} else 	GoToTargetWithAnimation ();
	}

	// Moving Charactere
	void StopMooving () {
		this.transform.position = this.targetClick;
		this.moving = false;
		this.asset.animatorSetSpeed (0);
		this.asset.animatorPlay ("Walking");
	}
	void GoToTargetWithAnimation () {
		this.asset.animatorSetSpeed (1);
		if (this.entityTarget) {
			float calculated = this.attack_distance;
			CircleCollider2D coll = GetComponent<CircleCollider2D> ();
			if (coll) calculated += coll.radius;
			coll = this.entityTarget.GetComponent<CircleCollider2D> ();
			if (coll) calculated += coll.radius;
			Vector3 entityTarget_position = new Vector3 (entityTarget.transform.position.x, entityTarget.transform.position.y, transform.position.z);
			if ((entityTarget_position - transform.position).magnitude < calculated) {
				asset.animatorTrigger ("Attack");
				asset.soundsAction("Attack");
				return;
			}
		}
		asset.animatorTrigger ("Walk");
	}

	bool CheckMoovingDistance (float a, float b) {
		return (a > b);
	}

	bool CheckPositionByDistance (float a, float b) {
		return (a <= b);
	}
	void MovingCharacter () {
		float direction = Vector3.Dot (Vector3.up, VectDirection (this.targetClick, this.transform.position));
		asset.animatorFloat ("Direction", direction);
		this.transform.localScale = SpriteScaleReverse (this.targetClick.x, this.transform.position.x);
		Vector3 displace = VectDirection (this.targetClick, this.transform.position) * speed * Time.deltaTime;
		float old_distance = (this.transform.position - targetClick).magnitude;
		if (CheckMoovingDistance (old_distance, displace.magnitude)) this.transform.position += displace;
		// if (CheckPositionByDistance(old_distance, displace.magnitude)) this.transform.position = this.targetClick - (VectDirection (this.targetClick, this.transform.position) * (ditance_trigger / 2));
		// if (CheckPositionByDistance(old_distance, displace.magnitude + ditance_trigger)) 
		else StopMooving ();
	}

	// Utils Sprites and other
	Vector3 VectDirection (Vector3 target, Vector3 pos) {
		return (target - pos).normalized;
	}
	Vector3 SpriteScaleReverse (float target, float pos) {
		return (target < pos ? new Vector3 (-1, 1, 1) : new Vector3 (1, 1, 1));
	}

	// newTarget With Manager
	public void newTarget (Vector3 point) {
		point.z = 0;
		this.targetClick = point;
		this.new_target = true;
	}
}