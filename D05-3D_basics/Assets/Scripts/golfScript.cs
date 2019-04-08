using UnityEngine;
using System.Collections;

public class golfScript : MonoBehaviour {

	public static golfScript instance { get; private set; }
	public Rigidbody rb;
	
	public SphereCollider hole;
	public GameObject arrow;

	void Awake () {
		if (!instance)
			instance = this;
	}

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	
}
