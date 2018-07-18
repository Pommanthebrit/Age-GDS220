﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummerEarth : MonoBehaviour {

	Animator anim;

	void Start () {
		anim = GetComponent<Animator> ();
		anim.SetBool ("cFireDead", true);
	}
	
	void Update () {
		if (Input.GetKey(KeyCode.Alpha1)) {
			//Earth
			anim.SetBool ("cFireDead", false);
		} else {
			anim.SetBool ("cFireDead", true);
		}
	}
}