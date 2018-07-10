using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour 
{
	public Player player;
	public float camSpeed = 0.2f;
	private float offset = -2.0f;
	private Vector3 camPosition;
    private bool shakeCamera = false;
    private int shakeCount = 0;

	private void Start () 
	{
		camPosition = transform.position;
	}
    public void ShakeCamera()
    {
        shakeCamera = true;
    }
	private void LateUpdate () 
	{
        if (!shakeCamera)
        {
            camPosition.x = player.transform.position.x;
            camPosition.y = player.transform.position.y - offset;
        }
        else
        {
            camPosition.x = player.transform.position.x + Random.Range(-1f, 1f);
            camPosition.y = player.transform.position.y-offset + Random.Range(-1f, 1f);
            shakeCount++;
            if (shakeCount > 5)
            {
                shakeCount = 0;
                shakeCamera = false;
            }
        }
        transform.position = camPosition;
    }
}
