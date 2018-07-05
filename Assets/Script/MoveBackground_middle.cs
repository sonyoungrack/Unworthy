﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground_middle : MonoBehaviour 
{

	public Player player;
	public float scrollSpeed = 0.03f;
	private Material thisMaterial;
	private Vector3 leftDistance;

	private void Start()
	{
		thisMaterial = GetComponent<Renderer>().material;
	}

	private void Update() 
	{

	}

	private void LateUpdate()
	{
		Vector3 newOffset = thisMaterial.mainTextureOffset;
		newOffset.Set(newOffset.x + (player.distance.x*scrollSpeed), newOffset.y, newOffset.z);
		thisMaterial.mainTextureOffset = newOffset;
	}
}
