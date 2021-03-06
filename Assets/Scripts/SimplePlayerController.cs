﻿using UnityEngine;
using System.Collections;

public class SimplePlayerController : MonoBehaviour
{

	public float speed = 12f;

	private bool facingLeft = true;

	private Rigidbody2D rb2d;
	private Animator anim;

	// Use this for initialization
	void Start ()
	{
		rb2d = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		float x = Input.GetAxis ("Horizontal");
		float y = Input.GetAxis ("Vertical");

		// Flip if neccessairy
		if ((x > 0 && facingLeft) || (x < 0 && !facingLeft)) //Falls geht nach Rechts aber guckt nach Links (und umgekehrt)			
			Flip ();

		// Set Running animation
		if (x != 0f || y != 0f)
			anim.SetBool ("Running", true);
		else
			anim.SetBool ("Running", false);

		bool attacking = anim.GetBool("Attacking");
		// Trigger Attacking
		if (Input.GetButtonDown ("Fire1") && !attacking)
			anim.SetTrigger ("Attacking");

		// If we're currently attacking, don't move around!
		if (!attacking) 
		{
			rb2d.AddForce (new Vector2 (x, y) * speed);
		}
	}

	void Flip ()
	{
		facingLeft = !facingLeft;
		Vector3 myScale = transform.localScale;
		myScale.x = myScale.x * -1; //myScale.x *= -1;
		transform.localScale = myScale;
	}

	public void SetSpeed(float speed)
	{
		this.speed = speed;
	}
}
