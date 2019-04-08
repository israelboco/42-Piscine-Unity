using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour {

    public string owner;
    public float destructTime = 3.0f;

    private float time;

	// Use this for initialization
	void Start () {
        time = 0f;
	}
	
	// Update is called once per frame
	void Update () {
	}

	private void FixedUpdate() {
        time += Time.deltaTime;
        if (time >= destructTime)
        {
            Destroy(this.gameObject);
        }
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
        Debug.Log("Bullet Collision");
        if (collision.gameObject.tag != owner && collision.gameObject.tag != tag && collision.gameObject.tag != "Weapon")
        {
            Destroy(this.gameObject);
        }
	}

}
