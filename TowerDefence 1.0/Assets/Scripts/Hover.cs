﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : Singleton<Hover> {

	private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
		this.spriteRenderer = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		FollowMouse ();
	}

	private void FollowMouse() {

		transform.position = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		transform.position = new Vector3 (transform.position.x, transform.position.y, 1);
	}

	public void Activate(Sprite sprite) {
	
		this.spriteRenderer.sprite = sprite;
		spriteRenderer.enabled = true;

	}

	public void Deactivate() {
		spriteRenderer.enabled = false;
		
	}

}
