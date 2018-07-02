using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicEffact : MonoBehaviour {
    public float smallingStart = 1f;
    private float delay = 0f;
    public float smallingSpeedPerSecend = 1;
	void Update () {
        delay += Time.deltaTime;
        if(smallingStart<=delay)
        {
            transform.localScale = new Vector3(transform.localScale.x-Time.deltaTime/smallingSpeedPerSecend,
                transform.localScale.y - Time.deltaTime / smallingSpeedPerSecend,
                transform.localScale.z - Time.deltaTime / smallingSpeedPerSecend);
            if(transform.localScale.x<=0)
            {
                Destroy(gameObject);
            }
        }
	}
}
