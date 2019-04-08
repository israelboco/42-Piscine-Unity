using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : MonoBehaviour {

	public bool human = false;
	public bool orc = false;
	public bool dead;

	public float speed = 3.0f;
	public float attack_distance = 0.1f;
	public float ditance_trigger = 0.0f;

	public HumanAnimations asset;
	public AttackEntity entitySelect;

	// private Animator animator;

	private bool new_target;
	private bool moving;
	private bool entityIsTarget;
	private Vector3 targetClick;

	void Start () {
		this.moving = false;
		this.asset.animatorSetSpeed (0);
		this.asset.animatorFloat ("Direction", -1);
		this.new_target = false;
		this.entityIsTarget = false;
		this.entitySelect = null;
		this.entityIsTarget = false;
		if (human) {
			ManagerHumans.instance.Add (this);
			ManagerOrcs.instance.AddToFightList (this);
		}
	}

	// Update is called once per frame
	void Update () {
		if (this.dead) return;
		if (this.new_target) SetAnimationAndTargeting ();
		if (this.entitySelect) SelectTarget ();
		if (this.entityIsTarget && this.entitySelect == null) {
			entityIsTarget = false;
			moving = false;
		}
		if (this.moving) movingCharacter ();
		if (!this.moving) {
			this.asset.animatorSetSpeed (0);
			this.asset.animatorPlay ("Walking");
		} else GoToTargetWithAnimation ();
	}

	void SetAnimationAndTargeting () {
		this.new_target = false;
		this.entitySelect = null;
		this.entityIsTarget = false;
		this.asset.soundsAction ("Acknowledge");
		this.asset.animatorPlay ("Walking");
		this.asset.animatorTrigger ("Walk");
		this.asset.animatorSetSpeed (1);
		this.moving = true;
	}

	void SelectTarget () {
		this.moving = true;
		this.entityIsTarget = true;
		this.targetClick = entitySelect.transform.position;
		this.targetClick.z = 0;
	}

	// Moving Charactere
	void StopMooving () {
		this.transform.position = this.targetClick;
		moving = false;
		asset.animatorSetSpeed (0.0f);
		asset.animatorPlay ("Walking");
	}
	void GoToTargetWithAnimation () {
		this.asset.animatorSetSpeed (1);
		if (this.entitySelect) {
			float calculated = attack_distance;
			CircleCollider2D coll = GetComponent<CircleCollider2D> ();
			if (coll) calculated += coll.radius;
			coll = entitySelect.GetComponent<CircleCollider2D> ();
			if (coll) calculated += coll.radius;
			Vector3 entitySelect_position = new Vector3 (entitySelect.transform.position.x, entitySelect.transform.position.y, transform.position.z);
			if ((entitySelect_position - transform.position).magnitude < calculated) {
				asset.animatorTrigger ("Attack");
				asset.soundsAction ("Attack");
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
	void movingCharacter () {
		float direction = Vector3.Dot (Vector3.up, VectDirection (this.targetClick, this.transform.position));
		asset.animatorFloat ("Direction", direction);
		this.transform.localScale = SpriteScaleReverse (this.targetClick.x, this.transform.position.x);
		Vector3 displace = VectDirection (this.targetClick, this.transform.position) * speed * Time.deltaTime;
		float old_distance = (this.transform.position - targetClick).magnitude;
		if (CheckMoovingDistance (old_distance, displace.magnitude)) this.transform.position += displace;
		// if (CheckPositionByDistance (old_distance, displace.magnitude)) this.transform.position = this.targetClick - (VectDirection (this.targetClick, this.transform.position) * (ditance_trigger / 2));
		// if (CheckPositionByDistance (old_distance, displace.magnitude + ditance_trigger))
		else StopMooving ();
	}

	// Utils Sprites and other
	Vector3 VectDirection (Vector3 target, Vector3 pos) {
		return (target - pos).normalized;
	}
	Vector3 SpriteScaleReverse (float target, float pos) {
		return (target < pos ? new Vector3 (-1, 1, 1) : new Vector3 (1, 1, 1));
	}

	/* Public  */
	// newTarget With Manager Public 
	public void newTarget (Vector3 point) {
		point.z = 0;
		this.targetClick = point;
		this.new_target = true;
	}
}