using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour 
{

	public Player player;
	public float camSpeed = 0.2f;
	private float offset = -2.0f;
	private Vector3 camPosition;

	private void Start () 
	{
		camPosition = transform.position;
	}

	private void LateUpdate () 
	{
		camPosition.x = player.transform.position.x;
		camPosition.y = player.transform.position.y - offset;

		transform.position = camPosition;
	}
}
