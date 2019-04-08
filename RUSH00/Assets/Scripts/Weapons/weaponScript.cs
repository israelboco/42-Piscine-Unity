using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponScript : MonoBehaviour {
    
    public Rigidbody2D bullet;
    public Sprite bulletSprite;
    public Sprite weaponTakedSprite;
    public Sprite weaponDroppedSprite;
    public string weaponName;
    public float fireRate = 0.5f;
    public float speedBullets = 50;
    public float timeBullets = 3;
    public int totalBullets = 20;
    public bool melee = false;
    public string owner;

    private float timer;
    private bool selected;
    private bool throwing;
    private Vector3 target;
    private float distance;
    private SpriteRenderer sprite;

	// Use this for initialization
	void Start () {
        sprite = GetComponent<SpriteRenderer>();

        timer = 0.0f;
        owner = null;
        selected = false;
        throwing = false;
        sprite.sprite = weaponDroppedSprite;
        sprite.sortingOrder = 1;
	}
	
	// Update is called once per frame
	void Update () {
	}

	void FixedUpdate()
	{
        timer += Time.deltaTime;
        if (throwing) {
            float tmp = Vector3.Distance(transform.position, target);
            if (tmp < distance) {
                distance = tmp;
            }
            GetComponent<Rigidbody2D>().angularVelocity = 500 * Vector3.Distance(transform.position, target);
            if (distance <= 1 || tmp > distance) {
                sleep();
            }
        }
	}

    public void Take(string tag) {
        selected = true;
        sprite.sprite = weaponTakedSprite;
        owner = tag;
        sprite.sortingOrder = 3;
        Physics2D.IgnoreLayerCollision(8, 0, true);
        Debug.Log("Take Weapon");
    }

    public void Drop() {
        selected = false;
        sprite.sprite = weaponDroppedSprite;
        if (owner == "Player")
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = 0;
            GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(Vector3.down * 20);
            GetComponent<Collider2D>().isTrigger = false;
            distance = Vector3.Distance(transform.position, target);
            throwing = true;
        }
    }

    public void DropDead()
    {
        selected = false;
        sprite.sprite = weaponDroppedSprite;
        sleep();
    }

    public void Fire(bool fireSounds)
    {
        // Instantiate the projectile at the position and rotation of this transform
        if(totalBullets < 0 && !melee && fireSounds){
            ManagerSounds.instance.Play("Reload");
        }
        if (totalBullets > 0 || melee)
        {
            if (timer >= fireRate)
            {
                Rigidbody2D clone;

                clone = Instantiate(bullet, transform.TransformPoint(new Vector3(0f, -0.5f, 0f)), transform.rotation);
                clone.transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.eulerAngles.z - 90);
                clone.GetComponent<bulletScript>().owner = owner;
                clone.GetComponent<bulletScript>().destructTime = timeBullets;

                if (bulletSprite)
                {
                    if(fireSounds)
                        ManagerSounds.instance.Play(weaponName);
                    clone.GetComponent<SpriteRenderer>().sprite = bulletSprite;
                }
                clone.velocity = transform.TransformDirection(Vector3.down * speedBullets);
                if (!melee)
                    totalBullets--;
                timer = 0.0f;
            }
        }
    }

	private void sleep() {
        throwing = false;
        owner = null;
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GetComponent<Rigidbody2D>().angularVelocity = 0;
        GetComponent<Collider2D>().isTrigger = true;
        Physics2D.IgnoreLayerCollision(8, 0, false);
        sprite.sortingOrder = 1;
    }
}